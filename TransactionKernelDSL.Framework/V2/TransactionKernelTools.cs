﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionKernelDSL.Framework.V2
{
    public static class TransactionKernelTools
    {
        /// <summary>
        /// Decodes a string value from "byteInputParam" byte array, with "intInputLenParam" bytes long
        /// </summary>
        /// <param name="byteInputParam"></param>
        /// <param name="intInputLenParam"></param>
        /// <returns>Returns the decoded string value, if possible</returns>
        public static string GetString(byte[] byteInputParam, int intInputLenParam)
        {
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            return enc.GetString(byteInputParam, 0, intInputLenParam);
        }
        /// <summary>
        /// Decodes a string value from "byteInputParam" byte array, with "intInputLenParam" bytes long. 
        /// Starts decoding from an offset "intOffsetParam" within input byte array
        /// </summary>
        /// <param name="byteInputParam"></param>
        /// <param name="intInputLenParam"></param>
        /// <param name="intOffsetParam"></param>
        /// <returns></returns>
        public static string GetString(byte[] byteInputParam, int intInputLenParam, int intOffsetParam)
        {
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            return enc.GetString(byteInputParam, intOffsetParam, intInputLenParam);
        }
        /// <summary>
        /// Encodes a string value to a byte array
        /// </summary>
        /// <param name="strSourceParam"></param>
        /// <returns>Returns the encoded byte array</returns>
        public static byte[] GetBytes(string strSourceParam)
        {
            //   System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();           
            //   return encoding.GetBytes(strSourceParam);
            return Encoding.Default.GetBytes(strSourceParam);
        }
        /// <summary>
        /// Encodes a string value to a byte array, which will be prefixed with LVAR
        /// </summary>
        /// <param name="strSourceParam"></param>
        /// <returns>Returns the encoded "LVAR" byte array</returns>
        public static byte[] GetLVarBytes(string strSourceParam)
        {
            byte[] payload = Encoding.Default.GetBytes(strSourceParam);
            byte[] result = new byte[payload.Length + 1];
            byte[] header = GetBcdBytes(payload.Length.ToString().PadLeft(2, '0'));
            Buffer.BlockCopy(header, 0, result, 0, 1);
            Buffer.BlockCopy(payload, 0, result, 1, payload.Length);
            return result;
        }

        /// <summary>
        /// Encodes a string value to a byte array, which will be prefixed with LLVAR
        /// </summary>
        /// <param name="strSourceParam"></param>
        /// <returns>Returns the encoded "LLVAR" byte array</returns>
        public static byte[] GetLLVarBytes(string strSourceParam)
        {
            //   System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();           
            //   return encoding.GetBytes(strSourceParam);
            byte[] payload = Encoding.Default.GetBytes(strSourceParam);
            byte[] result = new byte[payload.Length + 2];
            byte[] header = GetBcdBytes(payload.Length.ToString().PadLeft(4, '0'));
            Buffer.BlockCopy(header, 0, result, 0, 2);
            Buffer.BlockCopy(payload, 0, result, 2, payload.Length);
            return result;
        }

        /// <summary>
        /// Encodes a string value with hexanumeric chars inside to a byte array
        /// </summary>
        /// <param name="strSourceParam"></param>
        /// <returns>Return the encoded byte array</returns>
        public static byte[] GetHexaBytes(string strSourceParam)
        {
         
            Dictionary<string, byte> trueMSNTable = new Dictionary<string, byte>() 
            { 
                {"0", 0x00},  {"1", 0x10}, {"2", 0x20}, {"3", 0x30}, {"4", 0x40},  
                {"5", 0x50},  {"6", 0x60}, {"7", 0x70}, {"8", 0x80}, {"9", 0x90}, 
                {"A", 0xA0},  {"a", 0xA0}, {"B", 0xB0}, {"b", 0xB0}, {"C", 0xC0}, 
                {"c", 0xC0},  {"D", 0xD0}, {"d", 0xD0}, {"E", 0xE0}, {"e", 0xE0},
                {"F", 0xF0},  {"f", 0xF0}
            };
            
            Dictionary<string, byte> trueLSNTable = new Dictionary<string, byte>() 
            { 
                {"0", 0x00}, {"1", 0x01}, {"2", 0x02}, {"3", 0x03}, {"4", 0x04},
                {"5", 0x05}, {"6", 0x06}, {"7", 0x07}, {"8", 0x08}, {"9", 0x09},
                {"A", 0x0A}, {"a", 0x0A}, {"B", 0x0B}, {"b", 0x0B}, {"C", 0x0C},
                {"c", 0x0C}, {"D", 0x0D}, {"d", 0x0D}, {"E", 0x0E}, {"e", 0x0E},
                {"F", 0x0F}, {"f", 0x0F}
            };

            if (strSourceParam.Length % 2 == 1)
            {
                strSourceParam += "0";
            }

            byte[] byteDestination = new byte[strSourceParam.Length / 2];

            for (int i = 0; i < byteDestination.Length; i++)
            {
                string strMostSignificantNibble = strSourceParam.Substring(2 * i, 1);
                string strLessSignificantNibble = strSourceParam.Substring((2 * i) + 1, 1);

                byteDestination[i] = trueMSNTable[strMostSignificantNibble];
                byteDestination[i] += trueLSNTable[strLessSignificantNibble];
            }

            return byteDestination;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pDest"></param>
        /// <param name="pSrc"></param>
        /// <param name="iLength"></param>
        /// <param name="iShift"></param>
        public static void AscToBcd(byte[] pDest, byte[] pSrc, int iLength, int iShift)
        {
            int i, intDest = 0, intSrc = 0;
            int intAux;
            byte bAux;
            pDest[0] = 0;
            iShift %= 2;
            for (i = iShift; i < (iLength + iShift); i++)
            {
                bAux = pSrc[intSrc];
                if (bAux > 57)
                {
                    intAux = bAux - 7;
                    bAux = (byte)intAux;
                }
                if ((i % 2) == 0)
                {
                    intAux = (pDest[intDest] & 0x0F) | ((bAux & 0x0F) << 4);
                    pDest[intDest] = (byte)intAux;
                }

                if ((i % 2) == 1)
                {
                    intAux = (pDest[intDest] & 0xF0) | (bAux & 0x0F);
                    pDest[intDest] = (byte)intAux;
                    intDest++;
                }


                intSrc++;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pDest"></param>
        /// <param name="pSrc"></param>
        /// <param name="iLength"></param>
        public static void AscToHex(byte[] pDest, byte[] pSrc, int iLength)
        {
            int i, intDest = 0, intSrc = 0;
            int intAux;
            pDest[0] = 0;
            for (i = 0; i < iLength; i++)
            {
                intAux = ValHex(pSrc[intSrc]) << 4;
                pDest[intDest] = (byte)intAux;
                intSrc++;
                intAux = pDest[intDest] | ValHex(pSrc[intSrc]);
                pDest[intDest] = (byte)intAux;
                intSrc++;
                intDest++;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Src"></param>
        /// <returns></returns>
        public static byte ValHex(byte Src)
        {

            if ((Src >= 0x30) && (Src <= 0x39))
            {
                return (byte)(Src - 0x30);
            }
            if ((Src >= 0x41) && (Src <= 0x46))
            {
                return (byte)(10 + Src - 0x41);
            }
            if ((Src >= 0x61) && (Src <= 0x66))
            {
                return (byte)(10 + Src - 0x61);
            }
            return 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pDest"></param>
        /// <param name="pSrc"></param>
        /// <param name="length"></param>
        /// <param name="iShift"></param>
        public static void BcdToAsc(byte[] pDest, byte[] pSrc, int length, int iShift, int offsetSrc = 0, int offsetDest = 0)
        {
            int i, intDest = offsetDest, intSrc = offsetSrc;
            int intAux;
            iShift %= 2;
            for (i = iShift; i < (length + iShift); i++)
            {
                if ((i % 2) == 0)
                {
                    intAux = ((pSrc[intSrc] & 0xF0) / 0x10) + 0x30;
                    pDest[intDest] = (byte)intAux;
                }
                if ((i % 2) == 1)
                {
                    intAux = (pSrc[intSrc] & 0x0F) + 0x30;
                    intSrc++;
                    pDest[intDest] = (byte)intAux;
                }
                if (pDest[intDest] > 0x39)
                {
                    intAux = pDest[intDest] + 7;
                    pDest[intDest] = (byte)intAux;
                }
                intDest++;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pAscii"></param>
        /// <param name="bData"></param>
        /// <param name="bBase"></param>
        public static void ByteToAsc(byte[] pAscii, byte bData, byte bBase)
        {
            int i;
            int intAux;

            for (i = 0; i < 2; i++)
            {
                intAux = (bData % bBase) + 0x30;
                pAscii[1 - i] = (byte)intAux;
                if ((bData % bBase) > 9)
                {
                    intAux = pAscii[1 - i] + 7;
                    pAscii[1 - i] = (byte)intAux;
                }

                intAux = (bData / bBase);
                bData = (byte)intAux;
            }
        }
        /// <summary>
        /// Devuelve la longitud hasta el primer caracter NULO (0x00) o hasta el tamaño del campo, el primer caso que suceda.
        /// </summary>
        /// <param name="byteFieldParam">Array de bytes a analizar</param>
        /// <returns>Longitud ASCII del campo (int)</returns>
        public static int GetLengthByField(byte[] byteFieldParam, int offset = 0)
        {
            int i = offset;
            for (i = offset; i < byteFieldParam.Length; i++)
            {
                if (byteFieldParam[i] == 0x00 /*&& i>1*/)
                {
                    break;
                }
            }

            return i;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="byteErrorCode"></param>
        /// <param name="intLengthParam"></param>
        /// <returns></returns>
        public static string GetAsciiString(byte[] byteErrorCode, int intLengthParam)
        {
            string strValue = "";
            for (int i = 0; i < intLengthParam; i++)
            {
                strValue += ((char)byteErrorCode[i]).ToString();
            }

            return strValue;
        }
        /// <summary>
        /// DEPRECADO - Usar GetAsciiString2
        /// </summary>
        /// <param name="byteErrorCode"></param>
        /// <param name="intLengthParam"></param>
        /// <param name="intOffsetParam"></param>
        /// <returns></returns>
        public static string GetAsciiString(byte[] byteErrorCode, int intLengthParam, int intOffsetParam)
        {
            string strValue = "";
            for (int i = intOffsetParam; i < intLengthParam; i++)
            {
                strValue += ((char)byteErrorCode[i]).ToString();
            }

            return strValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="byteErrorCode"></param>
        /// <param name="intLengthParam"></param>
        /// <param name="intOffsetParam"></param>
        /// <returns></returns>
        public static string GetAsciiString2(byte[] byteErrorCode, int intLengthParam, int intOffsetParam = 0)
        {
            string strValue = "";
            for (int i = intOffsetParam; i < (intLengthParam + intOffsetParam); i++)
            {
                strValue += ((char)byteErrorCode[i]).ToString();
            }

            return strValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="BcdStringParam"></param>
        /// <returns></returns>
        public static byte[] GetBcdBytes(string BcdStringParam)
        {
            byte[] byteBcd = new byte[BcdStringParam.Length / 2];

            if (BcdStringParam.Length % 2 == 1)
            {
                BcdStringParam = "0" + BcdStringParam;
            }

            TransactionKernelTools.AscToBcd(byteBcd, TransactionKernelTools.GetBytes(BcdStringParam), BcdStringParam.Length, 0);

            return byteBcd;
        }


        public static string GetBcdString(byte[] p, int p_2)
        {
            byte[] array = new byte[p_2 * 2];
            TransactionKernelTools.BcdToAsc(array, p, p_2 * 2, 0);
            return TransactionKernelTools.GetString(array, array.Length);
        }

        public static string GetBinString(byte[] p, int p_2, int offset = 0)
        {
            string strDump = "";

            for (int i = offset; i < (p_2 + offset); i++)
            {
                strDump += p[i].ToString("X").PadLeft(2, '0');
            }

            return strDump;
        }
        
    }
}
