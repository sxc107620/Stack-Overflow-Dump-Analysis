Stack-Overflow-Dump-Analysis
============================

CS 6356 (Software Maintenance) Project - The MSR 2015 Challenge: Analysis of Stack Overflow Dump

Our current project is to analyze the top answers on Stack Overflow (As determined by their score) and the Accepted Answers against a predefined set of metrics to determine what makes for a good answer. The dump we are using can be downloaded from the Internet Archive at https://archive.org/details/stackexchange. Details of the MSR 2015 Challenge can be found at http://2015.msrconf.org/challenge.php. In order to turn the Stack Overflow dump into a queryable database, we used SODDI (https://github.com/peschkaj/soddi) to import the XML into Microsoft SQL Server. All code written for this project will find its way into the repository. This README file will contain a rough schedule of the project, updated as we do things

Current metrics considered: 
1) Presence of Code Snippets
2) Length of Answer
3) Answerer's reputation
4) Presence of links in the answer
5) Response Time (Time between question posting and answer posting)

The project members are Scott Crain (github sxc107620), Niranjana Gajjelli and Marc Hudak. The professor of the course and overseer of the project is Andrian Marcus.

Project Updates:
10/19 - Data imported into SQL Server
10/21 - GitHub repository established, initial creation of this README

Upcoming Deliverables:
10/27 - One page finalized project description
