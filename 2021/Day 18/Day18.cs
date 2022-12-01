using System;

//This one really got away from me. I think there are cleaner ways to implement this
//Actually keeping these as strings instead of trees might be cleaner - given how tricky finding "the next node to the right/left" is
//But I'm short on time and this likely won't get reused so I'm not cleaning it up
public class AdventOfCodeDay18
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2021/Day 18/Problem1Input.txt");
        int magnitude = Problem1(lines);
        int largestSum = Problem2(lines);
        Console.WriteLine("Day 18 - Problem 1: The magnitude of the final sum is " + magnitude);
        Console.WriteLine("Day 18 - Problem 2: The magnitude of the largest sum is " + largestSum);
    }

    private static int Problem1(string[] lines)
    {
        List<Node> treeList = ParseInput(lines);
        Node finalTree = AddAllNumbers(treeList);
        return SumTree(finalTree);
    }

    private static int Problem2(string[] lines)
    {
        List<Node> treeList = ParseInput(lines);
        return FindLargestSum(treeList);
    }

    private static int FindLargestSum(List<Node> treeList)
    {
        int largestSum = 0;
        Node largestNode = new Node();
        for (int i = 0; i < treeList.Count - 1; i++)
        {
            for (int j = i + 1; j < treeList.Count; j++)
            {
                Node firstOrder = AddTwoNodes(new Node(treeList[i]), new Node(treeList[j]));
                Reduce(firstOrder);
                int firstOrderSum = SumTree(firstOrder);
                if (firstOrderSum > largestSum)
                {
                    largestSum = firstOrderSum;
                    largestNode = firstOrder;
                }
                Node secondOrder = AddTwoNodes(new Node(treeList[j]), new Node(treeList[i]));
                Reduce(secondOrder);
                int secondOrderSum = SumTree(secondOrder);
                if (secondOrderSum > largestSum)
                {
                    largestSum = secondOrderSum;
                    largestNode = secondOrder;
                }
            }
        }
        return largestSum;
    }

    private static Node AddAllNumbers(List<Node> treeList)
    {
        Node ret = AddTwoNodes(treeList[0], treeList[1]);
        Reduce(ret);
        for (int i = 2; i < treeList.Count; i++)
        {
            ret = AddTwoNodes(ret, treeList[i]);
            Reduce(ret);
        }
        return ret;
    }

    private static void PrintOneTree(Node tree)
    {
        if (tree.Left == null && tree.Right == null)
        {
            Console.Write(tree.Value);
        }
        if (tree.Left != null)
        {
            Console.Write('[');
            PrintOneTree(tree.Left);
            Console.Write(',');
        }
        if (tree.Right != null)
        {
            PrintOneTree(tree.Right);
            Console.Write(']');
        }
    }

    private static Node AddTwoNodes(Node left, Node right)
    {
        Node ret = new Node();
        ret.Left = left;
        ret.Right = right;
        left.Parent = ret;
        right.Parent = ret;
        return ret;
    }

    private static void Reduce(Node node)
    {
        bool reduced = false;
        while (!reduced)
        {
            TryExplode(node, 0);
            if (!TrySplit(node))
            {
                reduced = true;
            }
        }
    }

    private static void TryExplode(Node node, int level)
    {
        if (node.Left != null)
        {
            if (node.Left.isLeaf)
            {
                if (level > 3)
                {
                    Explode(node);
                    return;
                }
            }
            else
            {
                TryExplode(node.Left, level + 1);
            }
        }
        if (node.Right != null)
        {
            if (node.Right.isLeaf)
            {
                if (level > 3)
                {
                    Explode(node);
                }
            }
            else
            {
                TryExplode(node.Right, level + 1);
            }
        }
    }

    private static void Explode(Node node)
    {
        if (node.Parent == null)
        {
            return;
        }
        if (node.Left != null)
        {
            AddToLeftNode(node, node.Left.Value);
        }
        if (node.Right != null)
        {
            AddToRightNode(node, node.Right.Value);
        }
        node.Left = null;
        node.Right = null;
        node.Value = 0;
        node.isLeaf = true;
    }

    private static bool TrySplit(Node node)
    {
        if (node.Left != null)
        {
            if (TrySplit(node.Left))
            {
                return true;
            }
        }
        if (node.Right != null)
        {
            if (TrySplit(node.Right))
            {
                return true;
            }
        }
        if (node.isLeaf)
        {
            if (node.Value > 9)
            {
                Split(node);
                return true;
            }
        }
        return false;
    }

    private static void Split(Node node)
    {
        node.Left = new Node();
        node.Left.Parent = node;
        node.Left.Value = (int)node.Value / 2;
        node.Left.isLeaf = true;
        node.Right = new Node();
        node.Right.Parent = node;
        node.Right.Value = (int)((node.Value / 2.0) + 0.5);
        node.Right.isLeaf = true;
        node.Value = -1;
        node.isLeaf = false;
    }

    private static void AddToRightNode(Node node, int val)
    {
        if (node.Parent == null)
        {
            //We're at the parent
            //Shouldn't happen
            return;
        }
        else if (node.Parent.Parent == null)
        {
            //We're right below the parent
            if (node.Parent.Right != null && node.Parent.Right.Equals(node))
            {
                //We went all the way up the tree - we exploded the rightmost node
                return;
            }
            else
            {
                if (node.Parent.Right != null)
                {
                    node = node.Parent.Right;
                    while (node.Left != null)
                    {
                        node = node.Left;
                    }
                    node.Value = node.Value + val;
                    return;
                }
            }
        }
        if (node.Parent.Right != null && !node.Parent.Right.Equals(node))
        {
            node = node.Parent.Right;
            while (node.Left != null)
            {
                node = node.Left;
            }
            node.Value = node.Value + val;
            return;
        }
        AddToRightNode(node.Parent, val);
    }

    private static void AddToLeftNode(Node node, int val)
    {
        if (node.Parent == null)
        {
            //We're at the parent
            //Shouldn't happen
            return;
        }
        else if (node.Parent.Parent == null)
        {
            //We're right below the parent
            if (node.Parent.Left != null && node.Parent.Left.Equals(node))
            {
                //We went all the way up the tree - we exploded the leftmost node
                return;
            }
            else
            {
                if (node.Parent.Left != null)
                {
                    node = node.Parent.Left;
                    while (node.Right != null)
                    {
                        node = node.Right;
                    }
                    node.Value = node.Value + val;
                    return;
                }
            }
        }
        if (node.Parent.Left != null && !node.Parent.Left.Equals(node))
        {
            node = node.Parent.Left;
            while (node.Right != null)
            {
                node = node.Right;
            }
            node.Value = node.Value + val;
            return;
        }
        AddToLeftNode(node.Parent, val);
    }

    private static int SumTree(Node node)
    {
        if (node.Left == null || node.Right == null)
        {
            return node.Value;
        }
        return (3 * SumTree(node.Left)) + (2 * SumTree(node.Right));

    }

    private static List<Node> ParseInput(string[] lines)
    {
        List<Node> inputTrees = new List<Node>();
        foreach (string line in lines)
        {
            inputTrees.Add(ParseOneLine(line));
        }
        return inputTrees;
    }

    private static Node ParseOneLine(string line)
    {
        Node tree = new Node();
        tree.Left = BuildChild(line, 1, tree);
        tree.Right = BuildChild(line, FindRightIndex(line, 1), tree);
        return tree;
    }

    private static Node BuildChild(string line, int index, Node parent)
    {
        Node ret = new Node();
        ret.Parent = parent;
        if (!line[index].Equals('['))
        {
            ret.Value = Convert.ToInt32(line[index].ToString());
            ret.isLeaf = true;
        }
        else
        {
            ret.Left = BuildChild(line, index + 1, ret);
            ret.Right = BuildChild(line, FindRightIndex(line, index + 1), ret);
        }
        return ret;
    }

    private static int FindRightIndex(string line, int leftIndex)
    {
        int openBracketCount = 0;
        int rightIndex = 0;
        for (int i = leftIndex; i < line.Length; i++)
        {
            if (line[i].Equals('['))
            {
                openBracketCount++;
            }
            if (line[i].Equals(']'))
            {
                openBracketCount--;
            }
            if (line[i].Equals(',') && openBracketCount == 0)
            {
                return i + 1;
            }
        }
        return rightIndex;
    }
}

public class Node
{
    public Node? Parent;
    public Node? Left;
    public Node? Right;
    public int Value;
    public bool isLeaf;

    public Node()
    {
        Value = -1;
        isLeaf = false;
    }

    public Node(Node node)
    {
        Value = node.Value;
        isLeaf = node.isLeaf;
        if (node.Left != null)
        {
            Left = new Node(node.Left);
            Left.Parent = this;
        }
        if (node.Right != null)
        {
            Right = new Node(node.Right);
            Right.Parent = this;
        }
    }
}