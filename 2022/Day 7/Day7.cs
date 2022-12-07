using System;
using System.Text;

public class AdventOfCode2022Day7
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2022/Day 7/Problem1Input.txt");

        long totalSize = Problem1(lines);
        long directoryToDelete = Problem2(lines);

        Console.WriteLine("Day 7 - Problem 1: The total size of directories under 100000 is " + totalSize + ".");
        Console.WriteLine("Day 7 - Problem 2: The size of the directory to delete is " + directoryToDelete + ".");
    }

    private static long Problem1(string[] lines)
    {
        DirectoryNode head = new DirectoryNode("/", null);
        DirectoryNode curNode = head;
        ParseTree(lines, curNode);
        //PrintTree(head);
        List<long> sizes = new List<long>();
        long headSize = CalcDirectorySizes(head, sizes);
        long totalSize = 0;
        foreach (long size in sizes)
        {
            if (size <= 100000)
            {
                totalSize += size;
            }
        }
        return totalSize;
    }

    private static long CalcDirectorySizes(DirectoryNode head, List<long> sizes)
    {
        long size = head.size;
        foreach (KeyValuePair<string, DirectoryNode> node in head.children)
        {
            long childSize = CalcDirectorySizes(node.Value, sizes);
            sizes.Add(childSize);
            size += childSize;
        }

        return size;
    }

    private static void ParseTree(string[] lines, DirectoryNode curNode)
    {
        int idx = 1;
        while (idx < lines.Length)
        {
            string[] line = lines[idx].Split(' ');
            if (line[0].Equals("$"))
            {
                if (line[1].Equals("cd"))
                {
                    DirectoryNode? nextNode = ParseChangeDirectory(line[2], curNode);
                    if (nextNode != null)
                    {
                        curNode = nextNode;
                    }
                    idx++;
                }
                else if (line[1].Equals("ls"))
                {
                    idx = ParseLS(lines, curNode, idx + 1);
                }
            }
            else
            {
                idx++;
            }
        }
    }

    private static DirectoryNode? ParseChangeDirectory(string directory, DirectoryNode curNode)
    {
        if (directory.Equals(".."))
        {
            return curNode.parent;
        }
        else
        {
            return curNode.children[directory];
        }
    }

    private static int ParseLS(string[] lines, DirectoryNode curNode, int idx)
    {
        //Already seen an LS command for this directory
        if (curNode.children.Count > 0)
        {
            while (idx < lines.Length)
            {
                if (lines[idx].Equals(String.Empty))
                {
                    return idx + 1;
                }
                if (lines[idx][0].Equals('$'))
                {
                    return idx;
                }
                idx++;
            }
            return idx;
        }
        while (idx < lines.Length)
        {
            if (lines[idx].Equals(String.Empty))
            {
                return idx + 1;
            }
            string[] line = lines[idx].Split(' ');
            if (line[0].Equals("$"))
            {
                return idx;
            }
            if (line[0].Equals("dir"))
            {
                DirectoryNode child = new DirectoryNode(line[1], curNode);
                curNode.children.Add(line[1], child);
            }
            else
            {
                curNode.size += Convert.ToInt32(line[0]);
            }
            idx++;
        }
        return idx;
    }

    private static void PrintTree(DirectoryNode head)
    {
        Console.WriteLine("Dir " + head.val + " with size of " + head.size);
        if (head.children.Count > 0)
        {
            Console.WriteLine("Children: ");
            foreach (KeyValuePair<string, DirectoryNode> kvp in head.children)
            {
                Console.Write(kvp.Key + " ");
            }
            Console.Write('\n');
            foreach (KeyValuePair<string, DirectoryNode> kvp in head.children)
            {
                PrintTree(kvp.Value);
            }
        }
    }
    private static long Problem2(string[] lines)
    {
        DirectoryNode head = new DirectoryNode("/", null);
        DirectoryNode curNode = head;
        ParseTree(lines, curNode);
        //PrintTree(head);
        List<long> sizes = new List<long>();
        long headSize = CalcDirectorySizes(head, sizes);
        long minSize = 30000000 - (70000000 - headSize);
        Console.WriteLine("Size of root directory is " + headSize + " so free space is " + minSize);
        long minSizeFound = long.MaxValue;
        foreach (long size in sizes)
        {
            if (size >= minSize)
            {
                minSizeFound = Math.Min(minSizeFound, size);
            }
        }
        return minSizeFound;
    }
}

public class DirectoryNode
{
    public string val;
    public long size;
    public Dictionary<string, DirectoryNode> children;

    public DirectoryNode? parent;

    public DirectoryNode(string name, DirectoryNode? Parent)
    {
        this.val = name;
        this.parent = Parent;
        this.children = new Dictionary<string, DirectoryNode>();
    }
}