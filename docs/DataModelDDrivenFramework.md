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

