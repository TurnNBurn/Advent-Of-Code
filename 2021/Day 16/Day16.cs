using System;
using System.Text;

public class AdventOfCodeDay16
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2021/Day 16/Problem1Input.txt");
        int versionSum = Problem1(lines);
        long packetExpression = Problem2(lines);
        Console.WriteLine("Day 16 - Problem 1: The sum of all the packet versions is " + versionSum);
        Console.WriteLine("Day 16 - Problem 2: The expression evaluates to " + packetExpression);
    }

    private static int Problem1(string[] lines)
    {
        string binaryString = ParseHexToBinary(lines[0]);
        List<Packet> packets = ParseBinary(binaryString);
        return SumVersions(packets);
    }

    private static long Problem2(string[] lines)
    {
        string binaryString = ParseHexToBinary(lines[0]);
        List<Packet> packets = ParseBinary(binaryString);
        return EvaluatePacketByType(packets[0]);
    }

    private static long EvaluatePacketByType(Packet pack)
    {
        switch (pack.Type)
        {
            case 0:
                long sum = 0;
                foreach (Packet subPacket in pack.SubPackets)
                {
                    sum += EvaluatePacketByType(subPacket);
                }
                return sum;
            case 1:
                long product = 1;
                foreach (Packet subPacket in pack.SubPackets)
                {
                    product *= EvaluatePacketByType(subPacket);
                }
                return product;
            case 2:
                long min = long.MaxValue;
                foreach (Packet subPacket in pack.SubPackets)
                {
                    min = Math.Min(min, EvaluatePacketByType(subPacket));
                }
                return min;
            case 3:
                long max = 0;
                foreach (Packet subPacket in pack.SubPackets)
                {
                    max = Math.Max(max, EvaluatePacketByType(subPacket));
                }
                return max;
            case 4:
                return pack.Literal;
            case 5:
                return EvaluatePacketByType(pack.SubPackets[0]) > EvaluatePacketByType(pack.SubPackets[1]) ? 1 : 0;
            case 6:
                return EvaluatePacketByType(pack.SubPackets[0]) < EvaluatePacketByType(pack.SubPackets[1]) ? 1 : 0;
            case 7:
                return EvaluatePacketByType(pack.SubPackets[0]) == EvaluatePacketByType(pack.SubPackets[1]) ? 1 : 0;
            default:
                return 0;
        }
    }

    private static int SumVersions(List<Packet> packetList)
    {
        int sum = 0;
        foreach (Packet pack in packetList)
        {
            sum += pack.Version;
            if (pack.Type != 4)
            {
                sum += SumVersions(pack.SubPackets);
            }
        }
        return sum;
    }

    private static List<Packet> ParseBinary(string binary)
    {
        List<Packet> packets = new List<Packet>();
        int index = 0;
        //Only loop while there are at least enough bits to construct a version and type
        while (index < binary.Length - 7)
        {
            string remainingBinary = binary.Substring(index);
            if (!remainingBinary.Contains('1'))
            {
                break;
            }
            Packet pack = ParseNextPacket(binary, ref index);
            packets.Add(pack);
        }
        return packets;
    }

    private static Packet ParseNextPacket(string binary, ref int index)
    {
        int version = Convert.ToInt32(binary.Substring(index, 3), 2);
        index += 3;
        int type = Convert.ToInt32(binary.Substring(index, 3), 2);
        index += 3;
        if (type == 4)
        {
            Packet literal = new Packet(version, type);
            index = ParseLiteral(binary, index, literal);
            return literal;
        }
        else
        {
            index = ParseOperator(binary, index, out List<Packet> subPackets);
            return new Packet(version, type, subPackets);
        }
    }

    private static int ParseLiteral(string binary, int index, Packet literal)
    {
        bool hasMoreBytes = true;
        int startIndex = index;
        StringBuilder sb = new StringBuilder();
        while (hasMoreBytes)
        {
            string nextByte = binary.Substring(index, 5);
            sb.Append(nextByte.Substring(1));
            if (nextByte[0].ToString().Equals("0"))
            {
                hasMoreBytes = false;
            }
            index += 5;
        }
        literal.Literal = Convert.ToInt64(sb.ToString(), 2);
        return index;
    }

    private static int ParseOperator(string binary, int index, out List<Packet> subPackets)
    {
        string lengthType = binary[index].ToString();
        index++;
        if (lengthType.Equals("0"))
        {
            //Next 15 bits contain the length of the sub packet
            int subPacketLength = Convert.ToInt32(binary.Substring(index, 15), 2);
            index += 15;
            subPackets = ParseBinary(binary.Substring(index, subPacketLength));
            return index + subPacketLength;
        }
        else
        {
            subPackets = new List<Packet>();
            int numSubpackets = Convert.ToInt32(binary.Substring(index, 11), 2);
            index += 11;
            for (int i = 0; i < numSubpackets; i++)
            {
                subPackets.Add(ParseNextPacket(binary, ref index));
            }
            return index;
        }
    }

    private static string ParseHexToBinary(string hex)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < hex.Length; i++)
        {
            sb.Append(HexToBinary[hex[i]]);
        }
        return sb.ToString();
    }

    private static Dictionary<char, string> HexToBinary = new Dictionary<char, string>{
        {'0',"0000"},
        {'1',"0001"},
        {'2',"0010"},
        {'3',"0011"},
        {'4',"0100"},
        {'5',"0101"},
        {'6',"0110"},
        {'7',"0111"},
        {'8',"1000"},
        {'9',"1001"},
        {'A',"1010"},
        {'B',"1011"},
        {'C',"1100"},
        {'D',"1101"},
        {'E',"1110"},
        {'F',"1111"}
    };
}

public class Packet
{
    public int Version;
    public int Type;
    public long Literal;
    public List<Packet> SubPackets;

    public Packet(int version, int type)
    {
        Version = version;
        Type = type;
        Literal = -1; //We might not track this data for problem 1 - probably a mistake
        SubPackets = new List<Packet>();
    }
    public Packet(int version, int type, int literal)
    {
        Version = version;
        Type = type;
        Literal = literal;
        SubPackets = new List<Packet>();
    }

    public Packet(int version, int type, List<Packet> subList)
    {
        Version = version;
        Type = type;
        Literal = -1; //Operators don't have literals
        SubPackets = new List<Packet>(subList);
    }
}
