using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{

    struct node
    {
        public RGBPixel parent;
        public int rank;
    }

    class MST
    {
        /// <summary>
        /// a class for finding the minimum spanning tree using kruskal algorithm
        /// </summary>
        static int edgesNumber;
        public static int j = 0;
        Edge[] kruskalArray;
        double sum = 0; //the sum of weights of the edges of the minimum spanning tree 
        public static Dictionary<RGBPixel, node> sets;   //to store each pixel and its parent

        int colorsNumber;
        public MST(graph Graph)
        {

            colorsNumber = Graph.getNumberOfColors();
            edgesNumber = colorsNumber - 1;
            sets = new Dictionary<RGBPixel, node>();

        }

        public void makeSet(graph Graph)
        {

            node n;
            for (int m = 0; m < colorsNumber; m++)
            {
                n.parent = Graph.getdistinct()[m];
                n.rank = 0;
                sets[n.parent] = n;
            }


        }
        public node findSet(RGBPixel p)
        {

            if (sets[p].parent.red != p.red || sets[p].parent.green != p.green || sets[p].parent.blue != p.blue)
            {

                sets[p] = findSet(sets[p].parent);  //path compression



            }

            return sets[p];

        }
        public bool unionSets(RGBPixel p, RGBPixel s)
        {
            node x = findSet(p);
            node y = findSet(s);
            if (x.parent.red != y.parent.red || x.parent.green != y.parent.green || x.parent.blue != y.parent.blue || x.rank != y.rank)
            {
                if (x.rank > y.rank)
                {
                    sets[s] = x;
                }
                else if (x.rank < y.rank)
                {
                    sets[p] = y;
                }
                else
                {
                    sets[p] = y;
                    y.rank++;
                    sets[s] = y;
                }

                return true;

            }
            return false;


        }
        public Edge[] getkruskalArray()
        {
            return kruskalArray;
        }

        public double kruskal(Edge[] edge, graph Graph)
        {
            makeSet(Graph);
            mergesort(edge, 0, Graph.getEdgesNumber() - 1);

            kruskalArray = new Edge[colorsNumber - 1];
            int size = Graph.getEdgesNumber();
            for (int i = 0; i < size; i++)
            {
                if (j == (colorsNumber - 1))
                {
                    break;
                }

                if (unionSets(edge[i].src, edge[i].destination) == true)
                {

                    kruskalArray[j].src = edge[i].src;
                    kruskalArray[j].destination = edge[i].destination;
                    kruskalArray[j].weight = edge[i].weight;


                    sum += kruskalArray[j].weight;
                    j++;

                }





            }



            return sum;




        }

        void merge(Edge[] arr, int l, int m, int r)
        {

            int n1 = m - l + 1;
            int n2 = r - m;


            Edge[] L = new Edge[n1];
            Edge[] R = new Edge[n2];
            int i, j;


            for (i = 0; i < n1; ++i)
            {
                L[i].src = arr[l + i].src;
                L[i].destination = arr[l + i].destination;
                L[i].weight = arr[l + i].weight;


            }



            for (j = 0; j < n2; ++j)
            {
                R[j].src = arr[m + 1 + j].src;
                R[j].destination = arr[m + 1 + j].destination;
                R[j].weight = arr[m + 1 + j].weight;

            }
            i = 0;
            j = 0;


            int k = l;
            while (i < n1 && j < n2)
            {
                if (L[i].weight <= R[j].weight)
                {
                    arr[k].src = L[i].src;
                    arr[k].destination = L[i].destination;
                    arr[k].weight = L[i].weight;

                    i++;
                }
                else
                {
                    arr[k].src = R[j].src;
                    arr[k].destination = R[j].destination;
                    arr[k].weight = R[j].weight;



                    j++;
                }
                k++;
            }


            while (i < n1)
            {
                arr[k].src = L[i].src;
                arr[k].destination = L[i].destination;
                arr[k].weight = L[i].weight;

                i++;
                k++;
            }

            while (j < n2)
            {
                arr[k].src = R[j].src;
                arr[k].destination = R[j].destination;
                arr[k].weight = R[j].weight;

                j++;
                k++;
            }
        }

        void mergesort(Edge[] edge, int l, int r)
        {

            if (l < r)
            {

                int m = (l + r) / 2;


                mergesort(edge, l, m);
                mergesort(edge, m + 1, r);


                merge(edge, l, m, r);
            }
        }










    }
}










