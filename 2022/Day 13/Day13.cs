using System;
using System.Text;

public class AdventOfCode2022Day13
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2022/Day 13/Problem1Input.txt");

        int inOrderSum = Problem1(lines);
        int decoder = Problem2(lines);

        Console.WriteLine("Day 13 - Problem 1: The sum of packets in order is " + inOrderSum + ".");
        Console.WriteLine("Day 12 - Problem 2: The decoder key is " + decoder + ".");
    }

    private static int Problem1(string[] lines)
    {
        List<Element> packets = new List<Element>();
        foreach (string line in lines)
        {
            if (!line.Equals(String.Empty))
            {
                packets.Add(ParseOnePacket(line.Substring(0, line.Length - 1)));
            }
        }
        List<int> inOrderIndexes = new List<int>();
        for (int i = 0; i < packets.Count - 1; i += 2)
        {
            if (Compare(packets[i], packets[i + 1]) < 0)
            {
                inOrderIndexes.Add((i / 2) + 1);
            }
        }
        int sum = 0;
        foreach (int i in inOrderIndexes)
        {
            sum += i;
        }
        return sum;
    }

    private static int Compare(Element first, Element second)
    {
        if (first.value != -1 && second.value != -1)
        {
            return first.value - second.value;
        }
        else if (first.value != -1)
        {
            Element containerElement = new Element();
            containerElement.value = -1;
            Element innerElement = new Element();
            innerElement.value = first.value;
            containerElement.list.Add(innerElement);
            return Compare(containerElement, second);
        }
        else if (second.value != -1)
        {
            Element containerElement = new Element();
            containerElement.value = -1;
            Element innerElement = new Element();
            innerElement.value = second.value;
            containerElement.list.Add(innerElement);
            return Compare(first, containerElement);
        }
        else
        {
            for (int i = 0; i < first.list.Count; i++)
            {
                if (i == second.list.Count)
                {
                    return 1;
                }
                int result = Compare(first.list[i], second.list[i]);
                if (result != 0)
                {
                    return result;
                }
            }
            //If we reached this point, the two elements are the same up to the end of first
            if (first.list.Count < second.list.Count)
            {
                return -1;
            }
            return 0; //Identical elements - shouldn't happen
        }
    }

    private static void PrintOnePacket(Element packet)
    {
        Console.Write('[');
        foreach (Element element in packet.list)
        {
            if (element.value != -1)
            {
                Console.Write(element.value);
            }
            else
            {
                PrintOnePacket(element);
            }
            Console.Write(',');
        }
        Console.Write(']');
    }

    private static Element ParseOnePacket(string packet)
    {
        Element element = new Element();
        element.value = -1;
        int index = 1;
        while (index < packet.Length)
        {
            if (packet[index].Equals('['))
            {
                int openCount = 1;
                int newPacketIndex = index;
                while (openCount > 0)
                {
                    index++;
                    if (packet[index].Equals('['))
                    {
                        openCount++;
                    }
                    else if (packet[index].Equals(']'))
                    {
                        openCount--;
                    }
                }
                element.list.Add(ParseOnePacket(packet.Substring(newPacketIndex, index - newPacketIndex)));
                index++; //Skip comma after list
            }
            else
            {
                int numBeginIndex = index;
                while (index < packet.Length && !packet[index].Equals(','))
                {
                    index++;
                }
                Element elementToAdd = new Element();
                elementToAdd.value = Convert.ToInt32(packet.Substring(numBeginIndex, index - numBeginIndex));
                element.list.Add(elementToAdd);
            }
            index++;
        }
        return element;
    }

    private static int Problem2(string[] lines)
    {
        List<Element> packets = new List<Element>();
        foreach (string line in lines)
        {
            if (!line.Equals(String.Empty))
            {
                packets.Add(ParseOnePacket(line.Substring(0, line.Length - 1)));
            }
        }
        Element two = new Element();
        two.value = -1;
        Element twoInner = new Element();
        twoInner.value = -1;
        Element twoInnerInner = new Element();
        twoInnerInner.value = 2;
        twoInner.list.Add(twoInnerInner);
        two.list.Add(twoInner);
        packets.Add(two);
        Element six = new Element();
        six.value = -1;
        Element sixInner = new Element();
        sixInner.value = -1;
        Element sixInnerInner = new Element();
        sixInnerInner.value = 6;
        sixInner.list.Add(sixInnerInner);
        six.list.Add(sixInner);
        packets.Add(six);
        packets.Sort(Compare);
        int twoIndex = 0;
        int sixIndex = 0;
        for (int i = 0; i < packets.Count; i++)
        {
            if (packets[i] == two)
            {
                twoIndex = i + 1;
            }
            if (packets[i] == six)
            {
                sixIndex = i + 1;
            }
        }
        return twoIndex * sixIndex;
    }

    private class Element
    {
        public List<Element> list;
        public int value;
        public Element()
        {
            list = new List<Element>();
        }
    }
}