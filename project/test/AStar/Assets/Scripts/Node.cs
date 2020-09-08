using Pathing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Node : IAStarNode
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

    public Node()
    {
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
        float result = 0;
        if (Math.Abs(((Node)goal).GetY() - this.GetY()) <= 1)
        {
            result = Math.Abs(((Node) goal).GetX() - this.GetX());
        } else if (Math.Abs(((Node) goal).GetX() - this.GetX()) <= 1)
        {
            result = Math.Abs(((Node) goal).GetY() - this.GetY());
        }
        else
        {
             result = (float) Math.Sqrt(Math.Pow(Math.Abs(((Node) goal).GetX() - this.GetX()), 2) +
                                       Math.Pow(Math.Abs(((Node) goal).GetY() - this.GetY()), 2));
             result = (float) Math.Round(result);
        }

        // List<Node> esmatedPath = heurial(new List<Node>(), this, goal);
        //  // heurial(esmatedPath, this, goal);
        //  if (esmatedPath.Count != 0)
        //  {
        //      foreach (var each in esmatedPath)
        //      {
        //          result += each.cost;
        //      }
        //  }
        // result = (float) Math.Sqrt(Math.Pow(Math.Abs(((Node) goal).GetX() - this.GetX()), 2) +
        //                            Math.Pow(Math.Abs(((Node) goal).GetY() - this.GetY()), 2));
        Debug.Log("起点: " + this.GetPositon() + " 终点: " + ((Node) goal).GetPositon() + "  预测距离: " + result);
        return result;
        throw new NotImplementedException();
    }

    private List<Node> heurial(List<Node> nodes,Node start,IAStarNode goal)
    {
        if (start.GetX() == ((Node) goal).GetX() && start.GetY() == ((Node) goal).GetY())
        {
            return nodes;
        }

        //
        Node min = new Node();
        int result = Int32.MaxValue;
        //找出最近的
        foreach (var each in start.neighbours)
        {
            
            if (each.GetX() == ((Node) goal).GetX() &&
                each.GetY() == ((Node) goal).GetY())
            {
                return nodes;
            }

            if (Math.Abs(((Node) goal).x - each.x) + Math.Abs(((Node) goal).y - each.y) < result)
            {
                result = Math.Abs(((Node) goal).x - each.x) + Math.Abs(((Node) goal).y - each.y);
                min = each;
            }
        }
        nodes.Add(min);
        return heurial(nodes, min, goal);
    }
}
