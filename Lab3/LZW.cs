using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace Lab3
{
    public interface ILZW
    {
        public byte[] Comprimir(byte[] uncompressed);
        public byte[] Descomprimir(byte[] decompress);
    }
    public class ImplementationClassL : ILZW
    {
        public byte[] Comprimir(byte[] uncompressed)
        {
            //atributos
            int nbytes = 0;
            List<byte> salida = new List<byte>(); 
            List<int> comp = new List<int>();
            byte[] w = new byte[1];
            Dictionary<byte, int> Leyenda = new Dictionary<byte, int>();
            //llenamos diccionario
            for (int i = 0; i < uncompressed.Length; i++)
            {
                if (!Leyenda.ContainsKey(uncompressed[i]))
                {
                    Leyenda.Add(uncompressed[i], Leyenda.Count + 1);
                }
            }
            //compresión
            Dictionary<byte[], int> Combinaciones = new Dictionary<byte[], int>();
            //tamaño del diccionario 
            int copydic = Leyenda.Count;
            foreach(byte k in uncompressed)
            {
                byte[] wk = new byte[1];
                wk.Concat(w);
                wk.Append(k);
                wk.ToArray();
                if (Combinaciones.ContainsKey(wk))
                {
                    w = wk;
                }
                else
                {
                    comp.Add(Combinaciones[w]);
                    Combinaciones.Add(wk, copydic++);
                    w = null;
                    w[0] = k;
                }
            }
            if (w == null)
            {
                comp.Add(Combinaciones[w]);
            }
            //lectura de bits 
            nbytes = Convert.ToInt32((uint)Math.Log(Leyenda.Count + Combinaciones.Count, 2.0) + 1);
            //composición inicial de salida
            salida.Add(Convert.ToByte(nbytes));
            salida.Add(Convert.ToByte(Leyenda.Count));
            for (int i = 1; i <= Leyenda.Count; i++)
            {
                salida.Add(Leyenda.FirstOrDefault( x => x.Value == i ).Key);
            }
            //de decimal a binario
            string codes = "";
            foreach(int y in comp)
            {
                codes += DecBin(y, nbytes);
            }
            //reagrupación de bytes
            string bfin = "";
            for (int i = 0; i < codes.Length; i++)
            {
                if(bfin.Length < 8)
                {
                    bfin += codes[i];
                }
                else
                {
                    long B = Convert.ToInt32(bfin);
                    salida.Add(Convert.ToByte(BinDec(B)));
                    bfin = codes[i].ToString();
                }
            }
            if (!string.IsNullOrEmpty(bfin))
            {
                while (bfin.Length < 8)
                {
                    bfin += 0;
                }
                long B = Convert.ToInt32(bfin);
                salida.Add(Convert.ToByte(BinDec(B)));
            }
            return salida.ToArray();
        }

        public string Descomprimir(string compressed)
        {
            //atributos
            int nbytes = Convert.ToInt32(compressed[0]);
            int origdic = Convert.ToInt32(compressed[1]);
            string codes = string.Empty;
            string salida = string.Empty;
            List<int> comp = new List<int>();
            string w;
            Dictionary<int, string> Leyenda = new Dictionary<int, string>();
            for (int i = 0; i < origdic; i++)
            {
                Leyenda.Add(Leyenda.Count + 1, compressed.Substring(2, origdic)[i].ToString());
            }
            foreach (char item in compressed.Substring(2 + origdic))
            {
                codes += DecBin(item, 8);
            }
            while (!string.IsNullOrEmpty(codes) && codes.Length >= nbytes)
            {
                comp.Add(Convert.ToInt32(codes.Substring(0, nbytes), 2));
                codes = codes.Substring(nbytes);
            }
            w = Leyenda[comp[0]];
            comp.RemoveAt(0);
            salida += w;
            foreach (var item in comp)
            {
                if (item != 0)
                {
                    string k = "";
                    if (Leyenda.ContainsKey(item))
                    {
                        k = Leyenda[item];
                    }
                    else if (item == Leyenda.Count)
                    {
                        k = w + w[0];
                    }
                    salida += k;
                    Leyenda.Add(Leyenda.Count + 1, w + k[0]);
                    w = k; 
                }
            }
            return salida;
        }

        string DecBin(int decim, int m)
        {
            long bin = 0;
            const int div = 2;
            long digit = 0;
            for (int i = decim % div, j = 0; decim > 0; decim /= div, i = decim % div, j++)
            {
                digit = i % div;
                bin += digit * (long)Math.Pow(10, j);
            }
            string Bin = bin.ToString();
            while (Bin.Length < m)
            {
                Bin = "0" + Bin;
            }
            return Bin;
        }

        int BinDec(long binary)
        {
            int decim = 0;
            int digit = 0;
            const int div = 10;

            for (long i = binary, j = 0; i > 0; i /= div, j++)
            {
                digit = (int)i % div;
                if (digit != 1 && digit != 0)
                {
                    return -1;
                }
                decim += digit * (int)Math.Pow(2, j);
            }
            return decim;
        }

        //int countBits(int n)
        //{
        //    int i = 0;
        //    while (n != 0)
        //    {
        //        i++;
        //        n >>= 1;
        //    }
        //    return i;
        //}
    }
}
