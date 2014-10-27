A basic program that runs stats on answers in the Stack Overflow Dump Database that I have. Written in C# and uses LINQ to communicate with the Database. The database must be set up on the local machine (Use SODDI with the most recent dump)

Computes five metrics (Wordcount, Link Presence, Code Snippet Presence, Answerer Reputation and Response Time) for each answer in the dataset and generates a CSV file, one line per answer.

Program execution is currently controlled by constant, global variables. This is because I simply run the program through Visual Studio. If we need a version that runs on the command line and accepts command line arguments (Or, worse yet, a GUI), I can do that. I don't anticipate the need so I have not done so yet.

A single run of this program can take anywhere from 20 minutes to three hours, depending on which dataset we're working with.

I compute accepted answer stats by taking 1/8 of the accepted answers (Grouped by ID) per run. Any fewer groups and I get an Out Of Memory exception. I determine how many Top answers to use by using the Mean Score (2) and Standard Deviation (12.77) even though scores don't follow a Normal Curve.

Any questions, email me at sxc107620@utdallas.edu or just ask me in person.

- Scott Crain