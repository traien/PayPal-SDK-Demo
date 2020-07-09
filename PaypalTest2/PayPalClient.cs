using System;
using PayPalCheckoutSdk.Core;
using PayPalHttp;

using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;

namespace PaypalTest2
{
    public class PayPalClient
    {
        /**
            Set up PayPal environment with sandbox credentials.
            In production, use LiveEnvironment.
         */
        public static PayPalEnvironment environment()
        {
            return new SandboxEnvironment("AVF0o5ZvqKS1HZAFqG66B13_laTDiUc1ljrGt6Bvz8CazAEVnNw1HgpfGM0_RNtvC-SNsLwel7dr8Mia",
                "EO-E-cHLjg_zHPabBIF6TCvgJyhnhZdh3Hkl1Jlf7b0AXg2Im2L4vPyBGShy1ekaJYld3Lb_6lVFB2By");
        }

        /**
            Returns PayPalHttpClient instance to invoke PayPal APIs.
         */
        public static HttpClient client()
        {
            return new PayPalHttpClient(environment());
        }

        public static HttpClient client(string refreshToken)
        {
            return new PayPalHttpClient(environment(), refreshToken);
        }

        /**
            Use this method to serialize Object to a JSON string.
        */
        public static String ObjectToJSONString(Object serializableObject)
        {
            MemoryStream memoryStream = new MemoryStream();
            var writer = JsonReaderWriterFactory.CreateJsonWriter(
                        memoryStream, Encoding.UTF8, true, true, "  ");
            DataContractJsonSerializer ser = new DataContractJsonSerializer(serializableObject.GetType(), new DataContractJsonSerializerSettings { UseSimpleDictionaryFormat = true });
            ser.WriteObject(writer, serializableObject);
            memoryStream.Position = 0;
            StreamReader sr = new StreamReader(memoryStream);
            return sr.ReadToEnd();
        }
    }
}