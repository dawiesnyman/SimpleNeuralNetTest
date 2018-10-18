# SimpleNeuralNetTest

So years ago I got interested in programming because I read about neural networks. This is my first serious attempt at writing one from scratch...

So far what I have used is the following table of training data:

| Input 1 | Input 2 | Input 3 | Output | 
| ------- |:-------:|:-------:| ------:|
| 0|0|1|0|
| 1|1|1|1|
| 1|0|1|1|
| 0|1|1|0|

and then test using the following:

| Input 1 | Input 2 | Input 3 | Output | 
| ------- |:-------:|:-------:| ------:|
|   1     |   0     |   0     |   1    |
|   0     |   0     |   0     |   0    |

As you can see the pattern is that the answer should be the same as "Input 1".

## Next steps
1. Make it easier to load values with expected output
2. Change weight adjustment to have option of using total weight, gradient etc (as if I know what I'm talking about :)
3. Ability to load and save network training (weights)

##Anna.Iris 
This uses the well known Iris dataset to predict a class. It work without hidden layers but fails when I add a hidden layer. -> back to the drawing board...
https://archive.ics.uci.edu/ml/datasets/Iris

https://github.com/adam-p/markdown-here/wiki/Markdown-Cheatsheet#tables