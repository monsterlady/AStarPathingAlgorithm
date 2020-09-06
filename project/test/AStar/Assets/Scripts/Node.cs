using Pathing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Node : IAStarNode
{
    private List<Node> neighbours;
    private float cost;
    private int x, y;

    public Node(int x,int y, float cost)
    {
        this.x = x;
        this.y = y;
        neighbours = new List<Node>();
        this.cost = cost;
    }

    public override string ToString()
    {
        int str = 0;
        string costV;
        string text = "";
        foreach (var node in neighbours)
        {
            str++;
            text += node.GetPositon();
        }

        if (GetCostDays() <  0)
        {
            costV = "Can't through it";
        }
        else
        {
            costV = cost + " days ";
        }

        return "Node(" + x + ", " + y + ") " +
               "Cost ：" + costV + " it has：" + str + " neighbors " + text;
    }


    public int GetX()
    {
        return x;
    }

    public int GetY()
    {
        return y;
    }

    public IEnumerable<IAStarNode> Neighbours => neighbours;

    public void AddNeighborToList(Node neighbor)
    {
        neighbours.Add(neighbor);
    }

    public float GetCostDays()
    {
        return cost;
    }

    public string GetPositon()
    {
        return  " Neighbor Node(" + GetX() + ", " + GetY() + ") ";
    }

    public float CostTo(IAStarNode neighbour) {
        return ((Node)neighbour).GetCostDays();
    }

    public float EstimatedCostTo(IAStarNode goal)
    {
        Debug.Log(this.ToString() + " goal: " + goal.ToString());
        Debug.Log(Math.Abs(((Node) goal).x - this.x) + Math.Abs(((Node) goal).y - this.y));
        return Math.Abs(((Node) goal).x - this.x -1) + Math.Abs(((Node) goal).y - this.y - 1);
        throw new NotImplementedException();
    }
}
