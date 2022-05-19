# Data Model Driven Framework

**Summary:** this whitepaper outlines considerations and best practices to support a data model driven approach using Azure Synapse Analytics

## Section 1: Introduction

### Purpose of this Whitepaper 

Over the last few years, many organisations have successfully shifted from a traditional data warehouse pattern to a modern data warehouse   paradigm. This allowed them to overcome some of the constraints around 3 V’s (velocity, volume, variety) of data, using an ELT (Extract, Load, Transform) approach to land relevant data in central data lakes.
Nowadays, we are seeing new paradigms such as Data Mesh and Lakehouse pattern emerging, which are looking to address some of the challenges around data sharing, data ownership, data duplication, cost optimisation and much more.
While substantial progress has been made around democratising the platforms for different personas (low-code vs code-first) and different skillsets (multiple programming languages – SQL, Python, R, Scala, .Net, etc.), an area that remains a challenge is the data understanding democratisation. The premises being, even if you know how to use the tools and have access to the data, if you cannot interpret it, the value you are able to drive from that data is very limited. Subject Matter Experts are required to understand the datasets as well as the relationships to other datasets.
In this document, we are introducing a Data Model Driven framework to help organisations offer all users a “navigation system” to their data lake. This framework aims to:

* Simplify ad-hoc data exploration,
* Standardise and democratise business knowledge and
* Promote cross-domains collaboration.

### Scope of this Whitepaper
In this whitepaper, we will be focusing on how to implement a Data Model Driven framework on Azure. Some of the technologies mentioned in this whitepaper are:

* Azure Data Lake Storage Gen 2
* Azure Synapse Analytics
* Azure Purview
* Azure DevOps and GitHub

## Section 2: Data Model Driven Framework

### Overview

The core idea of the framework is “Model Once, Query Easily”. The **objective** is to:

* Build a centralised model management store (Hub)
* Build a federated consumption pipeline management (Spoke)

The expected **benefits** are:

* Streamline/standardize data modelling tools and processes across the organisation,
* Ability to share/sync technical and business metadata definitions across multiple data model repos and tools,
* Ability to release specific business area entities to specific synapse workspaces,
* Ability to understand impact of specific changes to existing models downstream as part of release management and
* Important for a serverless/lakehouse approach, ability to easily query/analyse data without the need to be a dataset SME or to have dataset SMEs on the team.

Since such capability is not available natively in the platform today, a custom solution needs to be built and implemented in the customer tenant to enable such functionality.

### Personas

There are multiple personas involved throughout the creation of a data model to its consumption. 

Please find below details on some of the key personas. Please note, depending on the size and complexity of each organisation, an individual might play the role of multiple personas.

| Persona   | Role Details |
|:---------------------------------|:------------|
| Data Steward                     | The data steward is a subject matter expert responsible for ensuring the data is properly governed: data is high quality, data is in compliance with business and/or regulatory obligations. <br> The data steward works closely with data owner to understand dataset requirements  when it comes to classification, use, protection and quality standards. <br> The data steward provides guidance to the data modeller and ultimately test/validates the logical model and physical model. |
| Data Modeller                    | The data modeller creates a logical data model based on business logic and processes. <br> These models are most often created before data gets ingested. Therefore, this persona often defines the baseline for downstream processes that are then executed by data engineers and other personas. <br> The data modelling environment is often separate from the data engineering environment. After defining the logical model, the data modeller generates a physical model in the modelling tool, which is then handed over to the data engineer. <br> Commonly, standardized modelling tools like Erwin or ER Studio are used within an organization. |
| Data Engineer                    | The data engineer uses the physical model definition and materializes it within the data processing environment. <br> Once the model is defined, data is ingested by the data engineer into the physical data model to build reporting tools on top of it. <br> In addition, the data engineer also consumes data from different sources within an organization. Often, this is a challenge as developers are often unaware of relationships of tables and columns stored within a modern Lakehouse pattern. |
| Data Product Lead / Data Analyst | The data product lead and/or data analyst are the consumers of these data models. <br> They should be able to easily find the data, determine which datasets should be used in order to answer a given question or solve a problem. |

### Approach

Our recommended approach is to start with a single, preferably green field, Data Landing Zone and extend the solution over time. The approach is broken down into three phases:

1.	Initial implementation of a Data Model Driven Framework for a given Business Group 
2.	Enhancements to initial process and development of rollout mechanism to Synapse workspaces
3.	Rollout of the Data Model Driven Framework across all Data Landing Zones

The following subsections describes the steps required to implement a Data Model Driven Framework within Azure using a phased approach.

**Phase 1 – Implement Data Model Driven Framework for a given Business Group**

![Data Model Driven Framework - Single Landing Zone](/docs/images/DataModelDrivenFramework-SingleLandingZone.png)

The first phase should be focussed on building the initial setup and processes for the Data Model Driven Framework of an organization. Some parts are usually well established, others need to be built and established to build a good foundation for the next phases.

1. **Create logical model(s):** The first part of the process is usually well established. Often, organizations have well established tools that they have been using for years for modelling technical and business data models.
    1. As part of this first phase, standardized tools such as ER Studio, Erwin or Arrow are used to define and materialize these models.
    2. After defining these models, they will get saved to a file share. Sometimes, corporations also have a company-wide model repository in place, where they register model definitions and updates.

2. **Convert logical models into Synapse Lake Database format:** After the model has been defined and stored using the established modelling tools, the model must be converted into the common Lake Database format and stored in a central Git repository.
    1. To achieve this next step, a converter must be built that maps the model defined in tools like ER Studio, Erwin or something similar onto the JSON structure defined by Lake Databases. If an organization uses a file share to store these models, it may be required to store these models also in a common XML format besides the native file format extension that these tools use. This can be achieved by using a bridge that comes with many of these tools. Building a consistent conversion from a common file format like XML will be much simpler that parsing the native file format like the ds1 format that ER Studio uses. Alternatively, if companies already have a model repository in place, conversion can also be achieved by using REST APIs of these repositories.
    2. Once the converter has been defined, it is time to start automating the process. To keep it simple, the initial process can be automated using CI/CD pipelines such as GitHub Actions or Azure DevOps Pipelines. The pipeline can initially rely on manual triggers and should accept as input any details required to load the model definition stored on a file share or in an existing model repository. After converting, the model needs to be checked into a common GitHub repository.
    3. The newly converted model should be stored in a new feature branch of the repository and a pull request (PR) should be opened to allow reviewing the new Lake Database definition. For the file structure, the repository should be using the file structure defined by Synapse. This allows connecting a shared Synapse workspace to this repository and enables model owners to login and review the Lake Database within the Synapse Studio UI.
    4. Before merging the PR, model owners should open the model definition in the connected Synapse workspace to make sure that no errors exists and all tables, properties and relationships have been defined as expected. Conversion errors should be fixed in this step. In addition, this step allows data owners to also map the tables to a location on the data lake.
    5. After the review, both the model owner and the model repository owner should approve the PR before merging into the main repository branch. Additional documentation should be added by the Data Modeller and/or the Data Steward to ensure that users understand the purpose of the model and get further details provided such as where the data can be found withing the Azure data platform and who is the owner of the data model.
    6. Once the Lake Database model has been merged it needs to be published within the shared Synapse workspace. If Purview is used in the customers data platform, Purview will pick up the table definitions within the lake databases and make these available in the data catalogue  .

3. **Publish relevant Lake Database to specific Synapse Workspace(s):** The last step in this first phase is about making these models available in other Synapse workspaces. This step is particularly important to prove the value of the Data Model Driven Framework.
    1. In this first phase, the step can be manual and model definitions can be copied from one repository to another to make the model available to other teams inside their Synapse workspaces. The respective JSON files for a model need to be copied, checked-in to the repository connected to the new Synapse workspace into the “databases” folder, merged into the main branch and then published. Once that is done, the data model becomes available for use in compute engines like Spark, dataflows and SQL Serverless.
    2. If more capacity is available in this first phase, some of this process can be automated using CI/CD pipelines such as GitHub Actions or Azure DevOps Pipelines.

**Phase 2 – Enhancing the Data Model Driven Framework and development of a Lake Database rollout process**

After the initial pilot phase has been successfully completed and value has been proved, customers should use the next phase to focus on enhancing and improving the Data Model Driven Framework. Following aspects should be optimized and tools should be built with the requirement that Data Lake models will have to be shared across subscription boundaries:

1. **Optimizing the Lake Database converter:** In this phase, the converter should be optimized to overcome common mistakes and errors identified in the conversion process. Organizations should closely collaborate with the modelling tool support teams to understand how some of the artifacts, such as tables, data types and relationships, can be extracted with high precision.
2. **Enhancing the conversion process:** To incentivize and encourage Data Modellers to convert their data models into Lake Databases and share them with other teams in a common repository, the conversion process must be simplified as much as possible. Therefore, customers should invest into tools to enable these personas and embed this process into their workflow. A simple application should be built using Power Platform, where Data Modellers can find their model inside the common repository and provide all details to initiate the model conversion process. For the backend process, some of the existing tools and pipelines in DevOps or GitHub can be reused.
3. **Enhancing the review process:** To make the review process and check-in of new models and new model versions smoother, Data Modellers should be automatically tagged on the PR and notified when the conversion completed successfully. The pull request should include a link to review the model to speed up the review process and make model updates available more quickly to users.
4. **Adding Model Versioning:** Lake Databases don’t support versioning today. Therefore, customers should think about a versioning strategy for updates to existing models. Toda,y this will have to be solved by adding a suffix to the schema describing the version of the model. More details and recommendations can be found below: Section 4. Data Models Versioning
5. **Publishing process of models to Synapse workspaces:** Lastly, a process should be established to roll out data models to workspaces. Users of other workspaces should be able to request a deployment of Lake Databases to their workspace to be able to reuse the schema and understand relationships and properties of the data model. Similar to the conversion process to Lake Databases, we are recommending to develop a Power App, where users can list existing models in the common Lake Database repository and request deployment of a model to their repository which is connected to a Synapse workspace.
6. **Approval process for Lake Database rollout:** If approvals are required before rolling out Lake Databases across Synapse workspaces, customers should add an approval process to their Power App. This will enable granular controls regarding which team is allowed to use the data model from the Lake Database repository.

**Phase 3 – Rollout of the Data Model Driven Framework Sharing and distribution of data models across all Data Landing Zones**

![Data Model Driven Framework - Multiple Landing Zones](/docs/images/DataModelDrivenFramework-MultipleLandingZones.png)

In phase 3 customers should focus on the successful rollout and scaling of the solution across all Data Landing Zones to realize the full value of the Data Model Driven Framework:

1. **Rollout across Data Landing Zones and Business units:** In the third phase, other business units should be onboarded and their specific models should be added to the common Lake Database repository. We are suggesting to keep a single model repository instead of splitting the repository by business unit or data domain. 

## Section 3: Model Conversion Tool

### Introduction

Many organizations have established standardized data modelling tools that are used by Data Modellers to define common and standardized data models across the organization or business units. 
A common standard allows customers to share models across projects and business units and enables reuse of existing artifacts. Common tools used across the industry are:

* ER Studio Architect
* Erwin Data Modeller

These well-established modelling tools have evolved into complex solutions over time that enable the definition of logical models with simple entities and relationships, but also the definition of complex business structures, data domains and business logic. In addition, these tools often support versioning across all defined artifacts and additional features, such as dedicated model repositories that can be hosted to share models within a business entity. 
Besides the aforementioned logical models, these tools also provide the functionality to generate physical models and DDLs to quickly create the schema and tables within the Data Warehouse of choice.

### Need for Data Model Conversion

Many of features that are offered by the tools mentioned in the introduction are not available in Synapse Lake Databases today. Due to this, most organizations that have well-established modelling tools will keep using them and will keep them as default choice for such tasks. These existing tools also allow customers to stay service agnostic and will enable free choice of tools and engines for the actual data processing task. Lastly, these tools are part of a well-established process on the customer side (though processes can be changed if necessary).

Another reason, why these existing tools will stay relevant is that many customers have been using them for years and therefore have a large number of existing models that they would like to continue using.
Industry-models are another interesting approach to the problem and try to fully remove the need for such tools and modelling tasks but based on experience most customers will not adopt these models provided by Synapse due to differences in how they run their business and already existing well-established data models that map their requirements onto data warehousing and engineering tools.

In summary, we need to offer our customers the possibility and flexibility to convert these existing and future models into Synapse Lake Databases in a simple and easy manner so that businesses do not end up in a large model migration project when adopting Lake Databases.

## Data Model Conversion Approach

To support the import of models through ER Studio and Erwin models, a converter needs to be written, that maps the definitions from these tools to the definition structure and supported artifacts of Lake Databases.

As a first step, a common model definition standard or model export format needs to be identified that incorporates all properties of the model such as entities, relationships, key definitions and more. In addition, the exported model must be parsable by common programming languages to allow mapping of artifacts onto the Lake Database structure.

The following export mechanisms have been identified to solve the customer problem and enable them to adopt Lake Databases with their existing models:

1. **Conversion of existing models using common export format:** Many modelling tools have the possibility to export models into a common format. These exports are usually handled via bridges that are often licensed separately. By using these export bridges, models are converted into a common definition structure that can then be exported into the Lake Database structure.
2. **Conversion of existing models using existing repository APIs:** Some customers have also licensed a model repository that they are using to store all models in a common data source. Through the use of APIs, models can be exported and converted into the Lake Database structure. This approach requires all internal users to check in models into the common model repository.

The exported model definitions then need to be mapped to the Lake database definition structure through custom code. Existing converters do not exist today, and a new solution needs to be built.

### Current state of the solution
The existing solution is a console application written in .NET 6 that handles the conversion of ER Studio models into Lake Databases.

The ER studio model needs to be exported as DSV model first and can then be provided as input to the console application.

The console application has two required input parameters:

| Input            | Possible Values             | Description                               |
|:-----------------|:----------------------------|:------------------------------------------|
| --file-path, -f  | C:\Users\myuser\mymodel.dsv | Specifies the path to the exported model. |
| --model-type, -m | “ErStudio”                  | Specifies the type of model.              |

The converter will load the DSV file and convert it into the following Lake Database structure:

* database/<database-name>.json
* database/<database-name>/table/<table-name-1>.json
* database/<database-name>/relationship/<relationship-1>.json

These files can then be checked into the repository that is connected to your Synapse workspace and will then be loaded by the Synapse Studio the next time you open up the website.

As a next step, we will explore a different conversion mechanism as well as the direct export from the ER Studio Team Server.

### Future Enhancements
The following enhancements are considered:
* Adding Frontend for the model conversion process in Power Platform.
* Automated workflows to push the model to a configured workspace

## Section 4: Data Models Versioning

Today, Lake Databases do not support versioning at any layer. It is also not possible to place models into subfolders, which is why folders cannot be used either to define a versioning strategy. Therefore, versioning must be implemented using database names.

A version suffix should be used to specify the version of a model. Semantic versioning (v{Major}.{Minor}.{Patch}) is recommended to measure the significance of updates between versions. This will mean the following:

* **Major:** Breaking changes between model versions.
* **Minor:** Non-breaking changes between model versions such as added tables or columns.
* **Patch:** Bugfixes, such as column type fixes.

Checking in the Lake Database definitions into git will allow tracking of changes and will also allow to identify potential issues when updating to a newer model version in a downstream Synapse workspace. Using different name suffixes to realize versioning will also allow to use multiple versions of a Lake Database side by side.

**Changes between versions should be documented by model owners, so that Data Engineers can consume change logs and identify potential issues when upgrading.** This should be done inside a wiki or within a README file inside the Lake Database repository.

Inside the repository, it is advised to use the following folder structure for Lake Database definitions:
{Domain}/{ModelName}/{Version}/database/{databaseName}/{tables}/…
                                                      /{relationships}/…
 
Outdated and deprecated Lake Database models should be removed over time from the common Lake Database repository. If users want to upgrade to a newer Lake Database version, access and rollout should be requested through the Power Platform application existing in the customer environment.
