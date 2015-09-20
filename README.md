# TravelingSalesman

I started this project to learn C# and as an introduction to artificial intelligence.  

####Thus far, this project contains:

###Traditional Traveling Salesman Problem (undirected edges)
1. A brute force algorithm (bruteFindFastest()) that will find a shortest loop between a set of nodes (appears to work with up to 17 nodes)
2.  A farthest insertion algorithm that starts with a 2 edge loop between the starting node and the node furthest from it. Nodes are then inserted sequentially by calculating the node nearest to the loop.
      - the method farthestInsertion() is visualized by and called in WindForm Form1.
      
###Traveling Saleman Variation (directed edges and no return to starting node)
1. A breadth first search algorithm (BFS()) finds the fastest route to the goal node by searching breadth first.
2. A depth first search algorithm (DFS()) finds the fastest route to the goal node by searching depth first.

*A map of directed edges must be created before running these two methods.
