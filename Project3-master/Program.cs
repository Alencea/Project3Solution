using System.Diagnostics.Metrics;

namespace Project3
{
    internal class Program
    {
        /// <summary>
        /// begining off program
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[][] jag = new int[20][];// sets up jagged arrat
            StreamReader sr = new StreamReader(Path.GetFullPath("inputJagged.csv"));// gets file file

            string temp = sr.ReadToEnd();// reads in file
            string[] sets = temp.Split("\n");// splits file based on new line
            //for loop to further split file
            for (int i = 0; i < jag.Length; i++)
            {
                string[] hold = sets[i].Split(',');//splits file and makes new array to hold split file
                int[] nums = new int[hold.Length];// makes array to hold numbers from file
                for (int j = 0; j < hold.Length && !string.IsNullOrWhiteSpace(hold[j]); j++)//for loop till hold is empty
                {
                    int item = int.Parse(hold[j]);// converts string to int
                    nums[j] = item;// places int in nums array
                }
                jag[i] = nums; //places nums array in jag array
            }
            int[][] sortedJag = new int[jag.Length][];// makes array of equal length to jag

            int k = 0;// makes int k and sets to 0
            foreach (int[] item in jag)// for loop that runs through jag
            {
                sortedJag[k] = MergeSort(item); // sends item through Mergesort places in sortedJag
                k++;//incriments k
            }

            foreach (int[] items in sortedJag)// for loop that runs through items in sortedJag
            {
                Console.WriteLine("[{0}]", string.Join(", ", items));// outputs items from sortedJag on line
            }

            Console.WriteLine("What number are you looking for?\r\nIf you do not enter anything or do not eneter a number,\r\n then the input will be 0");// askes question
            string thing = Console.ReadLine();//waits for answer
            int find = 0;// makes find and sests to 0

            if (string.IsNullOrEmpty(thing) || thing.Length < 1 || !int.TryParse(thing, out find))// checks if thing is empty has a length less than 1 and is an int
            {
                Console.WriteLine("You have entered: 0");// outputs statment
            }
            else // else
            {
                find = int.Parse(thing);// conversts string to int
                Console.WriteLine("you have entered: " + find);// outputs statmens and what the user entered
            }
            
            int foundAt;// makes foundAt
            int l = 0;// makes l and sets it to 0
            foreach (int[] item in sortedJag)// for loop that runs item through sortedJag
            {
                foundAt = BinarySearch(sortedJag[l], find);// runs sortedJag through BinarySearch and places the index in foundAt
                Console.WriteLine("in array "+l+" the number "+find+" is found at: "+foundAt);// outputs statment with what array find was in what find was and foundAt
                l++;//incriments l
            }

            

        }
        
        //MergeSort splits array items returns array items
        static int[] MergeSort(int[] items)
        {
            if (items.Length <= 1)// checks if length of array is less than or equal to one 
            {
                return items;// returns items
            }
            int[] nums1 = new int[items.Length / 2];// makes array nums1 sets at half the length of items
            int[] nums2;// makes array nums2
            if (items.Length % 2 == 0)// checks if length of items is even or odd
            {
                nums2 = new int[items.Length / 2];// nums2 set at half the length of items
            }
            else// if items length is odd
            {
                nums2 = new int[items.Length / 2 + 1];// nums2 set at half + 1 length of items
            }
            Array.Copy(items, 0, nums1, 0, nums1.Length);// copies left half of items
            Array.Copy(items, items.Length / 2, nums2, 0, nums2.Length);// copies right half of items
            
            nums1 = MergeSort(nums1);// recusion of nums1 into MergeSort sets as nums1
            nums2 = MergeSort(nums2);// recusion of nums2 into MergeSort sets as nums2
            items = mergy(nums1, nums2);// sends nums1 and nums2 into mergy sets as items
            return items;// returns items
        }

        // merges ints nums1 and nums2
        static int[] mergy(int[] nums1, int[] nums2)
        {
            int[] items = new int[nums1.Length + nums2.Length];// makes array items sets length equalt length of nums1 and nums2
            for (int i = 0, j = 0, k = 0; (i < nums1.Length || j < nums2.Length) && k <= items.Length;)//runs throughnums1 and nums2
            {
                if(i < nums1.Length && j < nums2.Length)// checks to see if nums1 or nums2 has an element
                {
                    if (nums1[i] <= nums2[j])// checks to see if nums2 at j is bigger than nums1 at i
                    {
                        items[k] = nums1[i];//sets items at k = nums1 at i
                        i++;//incriments i
                        k++;//incriments k
                    }
                    else// if nums2 at j is less than nums1 at i
                    {
                        items[k] = nums2[j];// sets items at k = nums2 at j
                        j++;// incroments j
                        k++;// incorments k
                    }
                }
                else if (i < nums1.Length)// else if nums2 is empy and i is less than the length of nums1
                {
                    items[k] = nums1[i];// sets items at k = to nums1 at i
                    i++;// incroments i
                    k++;// incroments k
                }
                else if (j < nums2.Length)// else if nums1 is empy and j is less than the length of nums2
                {
                    items[k] = nums2[j];// sets items at k = to nums2 at j
                    j++;// incroments j
                    k++;// incroments k
                }
            }
            return items;// returns items
        }
        // sets up a binary search needing arrays items and int find returns int foundAt
        static int BinarySearch(int[] items, int find)
        {
            int foundAt = -1;// makes foundAt sets to -1
            if (items.Length >= 2 && items[items.Length / 2] == find)// checks if find is in the middle of items
            {
                foundAt = items.Length/2;// sets foundAt to the middle index of items
                return foundAt;// returns foundAt
            }
            else if (items.Length >= 2 && items[items.Length / 2] >= find)// else checks if the length of item is greater or equal to 2 then if the item at the middle index is greater then or equal to find
            { 
                int[] holder;// makes array holder
                if (items.Length % 2 == 0) // checks if the length of items is even
                { 
                    holder = new int[items.Length / 2];// sets holder to half the length of items
                }
                else// else
                {
                    holder = new int[(items.Length / 2) + 1];// sets holder to half +1 the length of items
                }
                Array.Copy(items, 0, holder, 0, holder.Length);// copies the left half of items into holder
                foundAt = BinarySearch(holder, find);// recursion sends holder and find back through BinarySearch
            }
            else if (items.Length >= 2 && items[items.Length/2] <= find)// else checks if the length of item is greater or equal to 2 then if the item at the middle index is less then or equal to find
            {
                int[] holder;// makes array holder
                if (items.Length % 2 == 0)
                {
                    holder = new int[items.Length / 2];// checks if the length of items is even
                }
                else// else
                {
                    holder = new int[(items.Length / 2) + 1];// sets holder to half the length of items
                }
                Array.Copy(items, items.Length/2, holder, 0, holder.Length);// copies the right half of items into holder
                foundAt = BinarySearch(holder,find);// recursion sends holder and find back through BinarySearch
            }
            else// else
            {
                if (items.Length == 1 && items[0] == find) // checks if items legth is equal to 1 and if the item in items is find
                {
                    foundAt = 0;// sets foundAt to 0
                }
            }
            if(foundAt != -1)// checks if foundAt is -1
            {
                foundAt = Array.IndexOf(items, find);// finds the index of find
            }

            return foundAt;// returns foundAt
        }
    }
}