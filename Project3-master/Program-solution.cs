using System.Diagnostics.Metrics;

namespace Project3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[][] jag = new int[20][];
            StreamReader sr = new StreamReader(Path.GetFullPath("inputJagged.csv"));

            string temp = sr.ReadToEnd();
            string[] sets = temp.Split("\n");
            
            for (int i = 0; i < jag.Length; i++)
            {
                string[] hold = sets[i].Split(',');
                int[] nums = new int[hold.Length];
                for (int j = 0; j < hold.Length && !string.IsNullOrWhiteSpace(hold[j]); j++)
                {
                    int item = int.Parse(hold[j]);
                    nums[j] = item;
                }
                jag[i] = nums; 
            }
            int[][] sortedJag = new int[jag.Length][];

            int k = 0;
            foreach (int[] item in jag)
            {
                sortedJag[k] = MergeSort(item);
                k++;
            }

            foreach (int[] items in sortedJag)
            {
                Console.WriteLine("[{0}]", string.Join(", ", items));
            }

            Console.WriteLine("What number are you looking for?\r\nIf you do not enter anything or do not eneter a number,\r\n then the input will be 0");
            string thing = Console.ReadLine();
            int find = 0;

            if (string.IsNullOrEmpty(thing) || thing.Length < 1 || !int.TryParse(thing, out find))
            {
                Console.WriteLine("You have entered: 0");
            }
            else 
            {
                find = int.Parse(thing);
                Console.WriteLine("you have entered: " + find);
            }
            
            int foundAt;
            int l = 0;
            foreach (int[] item in jag)
            {
                foundAt = BinarySearch(sortedJag[l], find);
                Console.WriteLine("in array "+l+" the number "+find+" is found at: "+foundAt);
                l++;
            }

            

        }

        static int[] MergeSort(int[] items)
        {
            if (items.Length <= 1)
            {
                return items;
            }
            int[] nums1 = new int[items.Length / 2];
            int[] nums2;
            if (items.Length % 2 == 0)
            {
                nums2 = new int[items.Length / 2];
            }
            else
            {
                nums2 = new int[items.Length / 2 + 1];
            }
            Array.Copy(items, 0, nums1, 0, nums1.Length);
            Array.Copy(items, items.Length / 2, nums2, 0, nums2.Length);
            
            nums1 = MergeSort(nums1);
            nums2 = MergeSort(nums2);
            items = mergy(nums1, nums2);
            return items;
        }

        static int[] mergy(int[] nums1, int[] nums2)
        {
            int[] items = new int[nums1.Length + nums2.Length];
            for (int i = 0, j = 0, k = 0; (i < nums1.Length || j < nums2.Length) && k <= items.Length;)
            {
                if(i < nums1.Length && j < nums2.Length)
                {
                    if (nums1[i] <= nums2[j])
                    {
                        items[k] = nums1[i];
                        i++;
                        k++;
                    }
                    else
                    {
                        items[k] = nums2[j];
                        j++;
                        k++;
                    }
                }
                else if (i < nums1.Length)
                {
                    items[k] = nums1[i];
                    i++;
                    k++;
                }
                else if (j < nums2.Length)
                {
                    items[k] = nums2[j];
                    j++;
                    k++;
                }
            }
            return items;
        }

        static int BinarySearch(int[] items, int find)
        {
            int foundAt = -1;
            if (items.Length >= 2 && items[items.Length / 2] == find)
            {
                foundAt = items.Length/2;
                return foundAt;
            }
            else if (items.Length >= 2 && items[items.Length / 2] >= find)
            { 
                int[] holder;
                if (items.Length % 2 == 0) 
                { 
                    holder = new int[items.Length / 2];
                }
                else
                {
                    holder = new int[(items.Length / 2) + 1];
                }
                Array.Copy(items, 0, holder, 0, holder.Length);
                foundAt = BinarySearch(holder, find);
            }
            else if (items.Length >= 2 && items[items.Length/2] <= find)
            {
                int[] holder;
                if (items.Length % 2 == 0)
                {
                    holder = new int[items.Length / 2];
                }
                else
                {
                    holder = new int[(items.Length / 2) + 1];
                }
                Array.Copy(items, items.Length/2, holder, 0, holder.Length);
                foundAt = BinarySearch(holder,find);
            }
            else
            {
                if (items.Length == 1 && items[0] == find)
                {
                    foundAt = 0;
                }
            }
            if(foundAt != -1)
            {
                foundAt = Array.IndexOf(items, find);
            }

            return foundAt;
        }
    }
}