using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lr_5
{
    internal class Program
    {
        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;
            public TreeNode(int x) { val = x; }
        }
        static void Main(string[] args)
        {
            //Те саме дерево
            Console.WriteLine($"Task1");
            TreeNode p = new TreeNode(1)
            {
                left = new TreeNode(2),
                right = new TreeNode(1)
            };
            Console.WriteLine($"Tree p");
            Print(p);
            TreeNode q = new TreeNode(1)
            {
                left = new TreeNode(2),
                right = new TreeNode(3)
            };
            Console.WriteLine($"\nTree q");
            Print(q);
            bool result = IsSameTree(p, q);
            Console.WriteLine($"\n{result}");
            //Симетричне дерево
            Console.WriteLine($"Task2");
            TreeNode Symmetrictree = new TreeNode(1)
            {
                left = new TreeNode(2)
                {
                    left = new TreeNode(3),
                    right = new TreeNode(4)
                },
                right = new TreeNode(2)
                {
                    left = new TreeNode(4),
                    right = new TreeNode(3)
                }
            };
            Console.WriteLine($"Tree ");
            Print(Symmetrictree);
            result = IsSymmetric(Symmetrictree);
            Console.WriteLine($"\n{result}");
            //Інвертувати бінарне дерево
            Console.WriteLine($"Task3");
            TreeNode Inverttree = new TreeNode(4)
            {
                left = new TreeNode(2)
                {
                    left = new TreeNode(1),
                    right = new TreeNode(3)
                },
                right = new TreeNode(7)
                {
                    left = new TreeNode(6),
                    right = new TreeNode(9)
                }
            };
            Console.WriteLine($"Tree ");
            Print(Inverttree);
            InvertTree(Inverttree);
            Console.WriteLine();
            Print(Inverttree);
            //K-й найменший елемент у BST
            Console.WriteLine($"\nTask4");
            TreeNode Ktree = new TreeNode(3)
            {
                left = new TreeNode(1)
                {
                    right = new TreeNode(2)
                },
                right = new TreeNode(4)
            };
            int k = 1;
            Console.WriteLine($"K = {k}\nTree ");
            Print(Ktree);
            int Result = KthSmallest(Ktree, k);
            Console.WriteLine($"\n{Result}");
            //Серіалізація та десеріалізація бінарного дерева
            Console.WriteLine($"Task5");
            TreeNode Serializetree = new TreeNode(1)
            {
                left = new TreeNode(2),
                right = new TreeNode(3)
                {
                    left = null,
                    right = new TreeNode(5)
                }
            };
            Console.WriteLine($"Tree ");
            Print(Serializetree);
            string serializedTree = Serialize(Serializetree);
            Console.WriteLine($"Serialized Tree: {serializedTree}");
            TreeNode deserializedTree = Deserialize(serializedTree);
            Console.WriteLine($"Deserialized Tree:");
            Print(deserializedTree);
            //Двійкове дерево Максимальна сума шляху
            Console.WriteLine($"\nTask6");
            TreeNode Sum = new TreeNode(1)
            {
                left = new TreeNode(2),
                right = new TreeNode(3)
            };
            Console.WriteLine($"Tree ");
            Print(Sum);
            Result = MaxPathSum(Sum);
            Console.WriteLine($"\n{Result}");
            //Камери бінарного дерева
            Console.WriteLine($"Task7");
            TreeNode CameraCovertree = new TreeNode(0)
            {
                left = new TreeNode(0),
                right = new TreeNode(0)
                {
                    left = null,
                    right = new TreeNode(0)
                }
            };
            Console.WriteLine($"Tree ");
            Print(CameraCovertree);
            Result = MinCameraCover(CameraCovertree);
            Console.WriteLine($"\n{Result}");
            //Вертикальний обхід бінарного дерева
            Console.WriteLine($"Task8");
            TreeNode VerticalTraversaltree = new TreeNode(3)
            {
                left = new TreeNode(9),
                right = new TreeNode(20)
                {
                    left = new TreeNode(15),
                    right = new TreeNode(7)
                }
            };
            Console.WriteLine($"Tree ");
            Print(VerticalTraversaltree);
            IList<IList<int>> res = VerticalTraversal(VerticalTraversaltree);
            Console.WriteLine();
            foreach (var level in res)
            {
                Console.Write("[");
                foreach (var value in level) { Console.Write($"{value}, "); }
                Console.WriteLine("]");
            } 
        }
        static void Print(TreeNode root)
        {
            if (root != null)
            {
                Print(root.left);
                Console.Write(root.val + " ");
                Print(root.right);
            }
        }
        //Те саме дерево
        static bool IsSameTree(TreeNode p, TreeNode q)
        {
            if (p == null && q == null) { return true; }
            if (p == null || q == null) { return false; }
            return p.val == q.val && IsSameTree(p.left, q.left) && IsSameTree(p.right, q.right);
        }
        //Симетричне дерево
        static bool SymmetricTree(TreeNode root1, TreeNode root2)
        {
            if (root1 is null && root2 is null) { return true; }
            if (root1?.val != root2?.val) { return false; }
            return SymmetricTree(root1?.left, root2?.right) &&
                   SymmetricTree(root1?.right, root2?.left);
        }
        static bool IsSymmetric(TreeNode root) { return SymmetricTree(root.left, root.right); }
        //Інвертувати бінарне дерево
        static TreeNode InvertTree(TreeNode root)
        {
            if (root != null)
            {
                var temp = root.left;
                root.left = InvertTree(root.right);
                root.right = InvertTree(temp);
            }
            return root;
        }
        //K-й найменший елемент у BST
        static int KthSmallest(TreeNode root, int k) { return Recursion(root, ref k); }
        static int Recursion(TreeNode node, ref int k)
        {
            if (node == null) { return -1; }
            var result = Recursion(node.left, ref k);
            k -= 1;
            if (k == 0) { return node.val; }
            if (k >= 1) { result = Recursion(node.right, ref k); }
            return result;
        }
        //Серіалізація та десеріалізація бінарного дерева
        static string Serialize(TreeNode root)
        {
            if (root == null) { return ""; }
            return root.val.ToString() + "," + Serialize(root.left) + "," + Serialize(root.right);
        }
        static TreeNode Deserialize(string serializedTree)
        {
            string[] tokens = serializedTree.Split(',');
            if (tokens.Length < 1 || string.IsNullOrEmpty(tokens[0])) { return null; }
            int val = int.Parse(tokens[0]);
            TreeNode root = new TreeNode(val);
            int currentPosition = 1;
            root.left = DeserializeHelper(tokens, ref currentPosition);
            root.right = DeserializeHelper(tokens, ref currentPosition);
            return root;
        }
        static TreeNode DeserializeHelper(string[] tokens, ref int currentPosition)
        {
            if (currentPosition >= tokens.Length || string.IsNullOrEmpty(tokens[currentPosition]))
            {
                currentPosition++;
                return null;
            }
            int val = int.Parse(tokens[currentPosition]);
            TreeNode node = new TreeNode(val);
            currentPosition++;
            node.left = DeserializeHelper(tokens, ref currentPosition);
            node.right = DeserializeHelper(tokens, ref currentPosition);
            return node;
        }
        //Двійкове дерево Максимальна сума шляху
        static int maxPathSum;
        static int MaxPathSum(TreeNode root)
        {
            maxPathSum = int.MinValue;
            FindMax(root, maxPathSum);
            return maxPathSum;
        }
        static int FindMax(TreeNode root, int maxPath)
        {
            if (root == null) { return 0; }
            var lMax = FindMax(root.left, maxPath);
            var rMax = FindMax(root.right, maxPath);
            var includingRoot = lMax + root.val + rMax;
            maxPathSum = Math.Max(maxPathSum, includingRoot);
            return Math.Max(0, Math.Max(lMax, rMax) + root.val);
        }
        //Камери бінарного дерева
        static int cameraCount = 0, Covered = 0, NeedCover = 1, HasCamera = 2;
        static int MinCameraCover(TreeNode root) { return PostOrderTraversal(root) == NeedCover ? ++cameraCount : cameraCount; }
        static int PostOrderTraversal(TreeNode root)
        {
            if (root == null) { return Covered; }
            int left = PostOrderTraversal(root.left);
            int right = PostOrderTraversal(root.right);
            if (left == NeedCover || right == NeedCover)
            {
                cameraCount++;
                return HasCamera;
            }
            else if (left == HasCamera || right == HasCamera) { return Covered; }
            else { return NeedCover; }
        }
        //Вертикальний обхід бінарного дерева
        static IList<IList<int>> VerticalTraversal(TreeNode root)
        {
            var columns = new Dictionary<int, List<int>>();
            var queue = new Queue<(TreeNode, int)>();
            queue.Enqueue((root, 0));
            int minCol = 0, maxCol = 0;
            while (queue.Count > 0)
            {
                var current = new Dictionary<int, List<int>>();
                var currentQueueCount = queue.Count;
                for (var i = 0; i < currentQueueCount; i++)
                {
                    (TreeNode node, int col) = queue.Dequeue();
                    if (!current.TryGetValue(col, out var values))
                    {
                        values = new List<int>();
                        current.Add(col, values);
                        minCol = Math.Min(minCol, col);
                        maxCol = Math.Max(maxCol, col);
                    }
                    values.Add(node.val);
                    if (node.left != null) { queue.Enqueue((node.left, col - 1)); }
                    if (node.right != null) { queue.Enqueue((node.right, col + 1)); }
                }
                foreach (var kvp in current)
                {
                    kvp.Value.Sort();
                    if (columns.TryGetValue(kvp.Key, out var values)) { values.AddRange(kvp.Value); }
                    else { columns.Add(kvp.Key, kvp.Value); }
                }
            }
            var res = new List<IList<int>>();
            for (var i = minCol; i <= maxCol; i++) { res.Add(columns[i]); }
            return res;
        }
    }
}
