using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ImageQuantization
{
    public struct Edge
    {
      public  RGBPixel src, destination;
     public   double weight;
     public int srcINDX, destINDX;

    }
   public class graph
    {
    public  static Edge[] edge;
        int colorsNumber;
        public static RGBPixel []distinct;
      public static  Dictionary<RGBPixel, int> distinctColor;//a dictionary to construct an undirected connected graph for the distinct colors in the image
     public static int edges=0;
    
     public graph()
      {
        
          distinctColor = new Dictionary<RGBPixel, int>();
          

      }
       public RGBPixel[] getdistinct()
     {
         return distinct;
     }
       public  void setDistinct(int x,int y)
       {

            distinct=new RGBPixel[x*y];
       }
      public void insertINDistinct(RGBPixel x,int y)
       {
           distinct[y] = x;
       }
        public Dictionary<RGBPixel, int> returnDistinctColor()
        {
            return distinctColor;
        }
       public void  setColorsNumber(int x)
        {
            this.colorsNumber = x;
        }
        public int getEdgesNumber()
     {
         return edges;
     }

        public int getNumberOfColors()
        {
            return colorsNumber;
        }

       
        public Edge[] GetEdge()
        {
            return edge;
        }
        public void constructGraph()
        {
            edge = new Edge[distinctColor.Count * distinctColor.Count];
           double count = 0;int j=0;
           double x, y, z; 
            for(int i=0;i<colorsNumber;i++)
            {
                for(int m=0;m<colorsNumber;m++)
                {
                    
                    if (m <= i)
                        continue;
                    x = distinct[i].red -distinct[m].red;
                    y = distinct[i].green - distinct[m].green;
                    z = distinct[i].blue - distinct[m].blue;
                    count = (x * x) + (y * y) + (z * z);
                    count = Math.Sqrt(count);




                    edge[j].src.red = distinct[i].red;
                    edge[j].src.green = distinct[i].green;
                    edge[j].src.blue = distinct[i].blue;

                    edge[j].destination.red = distinct[m].red;
                    edge[j].destination.green = distinct[m].green;
                    edge[j].destination.blue = distinct[m].blue;

                    edge[j].weight = count;
                    edge[j].srcINDX = i;
                    edge[j].destINDX = m;
                    edges++;
                    j++;
                  



                }
            }




          



                  
           
                



          






      }





    }
}
