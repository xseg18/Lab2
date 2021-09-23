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
        public string Comprimir(string compress);
        public string Descomprimir(string decompress);
    }
    public class ImplementationClassL : ILZW
    {
        public string Comprimir(string compress)
        {
            //atributos
            int nbytes = 0;
            int origdic = 0;
            string salida = "";
            List<int> comp = new List<int>();
            string w = "";
            Dictionary<string, int> Leyenda = new Dictionary<string, int>();
            //llenamos diccionario
            for (int i = 0; i < compress.Length; i++)
            {
                if (!Leyenda.ContainsKey(compress[i].ToString()))
                {
                    Leyenda.Add(compress[i].ToString(), i + 1);
                }
            }
            origdic = Leyenda.Count();
            //compresión
            foreach(char k in compress)
            {
                string wk = w + k;
                if (Leyenda.ContainsKey(wk))
                {
                    w = wk;
                }
                else
                {
                    comp.Add(Leyenda[w]);
                    Leyenda.Add(wk, Leyenda.Count + 1);
                    w = k.ToString();
                }
            }
            if (!string.IsNullOrEmpty(w))
            {
                comp.Add(Leyenda[w]);
            }

            //lectura de bits 
            nbytes = Convert.ToInt32(Math.Log(Leyenda.Count, 2));
            //composición inicial de salida
            salida += Convert.ToChar(nbytes);
            salida += Convert.ToChar(origdic);
            for (int i = 1; i <= origdic; i++)
            {
                salida += Leyenda.FirstOrDefault( x => x.Value == i ).Key;
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
                    salida += Convert.ToChar(BinDec(B));
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
                salida += Convert.ToChar(BinDec(B));
            }
            return salida;
        }

        public string Descomprimir(string decompress)
        {
            //atributos
            int nbytes = 0;
            int origdic = 0;
            string salida = "";

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
    }
}
