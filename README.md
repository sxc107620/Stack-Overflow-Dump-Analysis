Stack-Overflow-Dump-Analysis
============================

CS 6356 (Software Maintenance) Project - The MSR 2015 Challenge: Analysis of Stack Overflow Dump
------------------------------------------------------------------------------------------------

Our project is to analyze the top answers on Stack Overflow (As determined by their score) and the Accepted Answers against a predefined set of metrics to determine what makes for a good answer. The dump we are using can be downloaded from the [Internet Archive](https://archive.org/details/stackexchange). Details of the MSR 2015 Challenge can be found [here](http://2015.msrconf.org/challenge.php). In order to turn the Stack Overflow dump into a queryable database, we used [SODDI](https://github.com/peschkaj/soddi) to import the XML into Microsoft SQL Server. All code written for this project will find its way into the repository. This README file will contain a rough schedule of the project, updated as we do things.
To do Part-of-Speech counting on Answers, we use the [Stanford Natural-Language Parser](http://nlp.stanford.edu/software/lex-parser.shtml) ported to .NET by [Sergey Tihon](https://github.com/sergey-tihon/Stanford.NLP.NET)


The project members are Scott Crain (github sxc107620), Niranjana Gajjelli (github niranjana1890) and Marc Hudak (github MHudak). The professor of the course and overseer of the project is Andrian Marcus.

###Current metrics considered: 
1. Number of Nouns in the Answer
2. Number of Verbs in the Answer
3. Number of Adjectives in the Answer
4. Total number of Words in the Answer
5. Presence of Code Snippets in the Answer
6. Presence of links in the Answer
7. Response Time (Time between question posting and answer posting)

###Project Updates:
* 10/19 - Data imported into SQL Server  
* 10/21 - GitHub repository established, initial creation of this README  
* 10/27 - Initial Metrics Parser added to repository (Stack Overflow CSV Generator)  
* 10/28 - One-Page Project Summary and other such documents added
* 11/6 - Metric List Updated, Metrics Parser updated
* 11/10 - Data Extraction Completed (See CSV Files for the data)

###Upcoming Deliverables:
* 11/12 - Progress Update
