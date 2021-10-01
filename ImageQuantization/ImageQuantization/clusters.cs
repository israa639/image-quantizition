using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{
    /// <summary>
    /// a class for clustring pixel and find representative color for each cluster 
    /// </summary>
    class clusters
    {
        public static int K = 0;
        Dictionary<RGBPixel, RGBPixel> representativeColor; //to store the representative for each cluster
        Dictionary<RGBPixel, int> clustersCount; //to store the number of colors of each parent to get the average by later
        MST mst;

        static int edges;
        public clusters(Edge[] kruskalArray, graph g)
        {
            this.mst = new MST(g);
            clustersCount = new Dictionary<RGBPixel, int>();

            edges = kruskalArray.Length;
            representativeColor = new Dictionary<RGBPixel, RGBPixel>();
            mst.makeSet(g);

        }
        public void detectClusterHelper(Edge[] kruskalArray, int l, int r, int k)// l-->start index  r-->end index
        {
            int clusterNumber = 0;
            int verticesNumber = r + 2;
            int s = 0; // to calculate the number of unions done
            while (clusterNumber != k && l < r)
            {

                bool unionDone = mst.unionSets(kruskalArray[l].src, kruskalArray[l].destination);
                if (unionDone == true)
                { s++; }
                clusterNumber = verticesNumber - s;

                l++;
            }

        }

        public void detectCluster(Edge[] kruskalArray, int k, graph g)
        {

            detectClusterHelper(kruskalArray, 0, edges - 1, k);//clustering  colors in groups

            int length = g.getNumberOfColors();


            //find the representative color of  each group
            for (int f = 0; f < length; f++)
            {
                RGBPixel x = mst.findSet(g.getdistinct()[f]).parent;
                if (!clustersCount.ContainsKey(x))
                {
                    clustersCount[x] = 1;
                    representativeColor[x] = g.getdistinct()[f];


                }
                else
                {
                    clustersCount[x]++;
                    RGBPixel y;
                    y = (representativeColor[x]);
                    y.red += g.getdistinct()[f].red;
                    y.blue += g.getdistinct()[f].blue;
                    y.green += g.getdistinct()[f].green;
                    representativeColor[x] = y;

                }


            }

        }


        public void findRepColors(RGBPixel[,] ImageMatrix, graph g)
        {
            RGBPixel x;
            for (int i = 0; i < ImageMatrix.GetLength(0); i++)
            {

                for (int j = 0; j < ImageMatrix.GetLength(1); j++)
                {

                    x = mst.findSet(ImageMatrix[i, j]).parent;
                    int y = clustersCount[x];
                    ImageMatrix[i, j].red = (byte)((representativeColor[x].red) / (y));
                    ImageMatrix[i, j].green = (byte)((representativeColor[x].green) / (y));
                    ImageMatrix[i, j].blue = (byte)((representativeColor[x].blue) / (y));



                }
            }





        }

    }
}
