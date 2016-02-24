using System;
using System.Xml;
using System.Xml.Schema;

namespace RegexAndXML
{
    /// <summary>
    /// A variety of examples of processing XML documents
    /// </summary>
    public static class XMLExamples
    {
        /// <summary>
        /// States used in the demo
        /// </summary>
        private static string[] states =
            {"Utah", "Nevada", "Arizona", "California", "Oregon", "Washington", "Idaho" };

        /// <summary>
        /// Capitals used in the demo
        /// </summary>
        private static string[] capitals =
            {"Salt Lake City", "Carson City", "Phoenix", "Sacramento", "Salem", "Olympia", "Boise"};

        /// <summary>
        /// Writes three XML documents then reads them back in
        /// </summary>
        public static void Main()
        {
            WriteExample1();
            WriteExample2();
           // WriteExample3();

            ReadExample1();
            Console.ReadLine();
            ReadExample2();
            Console.ReadLine();
            ReadExample3();
            Console.ReadLine();
        }

        /// <summary>
        /// Uses nested elements exclusively
        /// </summary>
        public static void WriteExample1()
        {
            using (XmlWriter writer = XmlWriter.Create("../../states1.xml"))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("States");

                for (int i = 0; i < states.Length; i++)
                {
                    writer.WriteStartElement("State");
                    writer.WriteElementString("Name", states[i]);
                    writer.WriteElementString("Capital", capitals[i]);
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }


        /// <summary>
        /// Uses attributes exclusively
        /// </summary>
        public static void WriteExample2()
        {
            using (XmlWriter writer = XmlWriter.Create("../../states2.xml"))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("States");

                for (int i = 0; i < states.Length; i++)
                {
                    writer.WriteStartElement("State");
                    writer.WriteAttributeString("Name", states[i]);
                    writer.WriteAttributeString("Capital", capitals[i]);
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        /// <summary>
        /// Like WriteExample2, except that it specifies a namespace for the root element 
        /// </summary>
        public static void WriteExample3()
        {
            using (XmlWriter writer = XmlWriter.Create("../../states3.xml"))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("", "States", "urn:states-schema");

                for (int i = 0; i < states.Length; i++)
                {
                    writer.WriteStartElement("State");
                    writer.WriteAttributeString("Name", states[i]);
                    writer.WriteAttributeString("Capital", capitals[i]);
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        /// <summary>
        /// Reads the file written by WriteExample1
        /// </summary>
        public static void ReadExample1()
        {
            using (XmlReader reader = XmlReader.Create("../../states1.xml"))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "States":
                                break;

                            case "State":
                                Console.WriteLine();
                                break;

                            case "Name":
                                Console.Write("State name = ");
                                reader.Read();
                                Console.WriteLine(reader.Value);
                                break;

                            case "Capital":
                                Console.Write("State capital = ");
                                reader.Read();
                                Console.WriteLine(reader.Value);
                                break;
                        }
                    }
                }

            }
        }

        /// <summary>
        /// Reads the file written by WriteExample2
        /// </summary>
        public static void ReadExample2()
        {
            using (XmlReader reader = XmlReader.Create("../../states2.xml"))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "States":
                                break;

                            case "State":
                                Console.WriteLine();
                                Console.WriteLine("State name = " + reader["Name"]);
                                Console.WriteLine("State capital = " + reader["Capital"]);
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Reads the file written by WriteExample3.  Just like ReadExample2, except it
        /// uses a schema to validate the file being read.
        /// </summary>
        public static void ReadExample3()
        {
            // Create the XmlSchemaSet class.  Anything with the namespace "urn:states-schema" will
            // be validated against states3.xsd.
            XmlSchemaSet sc = new XmlSchemaSet();

            // NOTE: To read states3.xsd this way, it must be stored in the same folder with the
            // executable.  To arrange this, I set the "Copy to Output Directory" propery of states3.xsd to
            // "Copy If Newer", which will copy states3.xsd as part of each build (if it has changed
            // since the last build).
            sc.Add(null, "states3.xsd");

            // Configure validation.
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas = sc;
            settings.ValidationEventHandler += ValidationCallback;

            using (XmlReader reader = XmlReader.Create("../../states3.xml", settings))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "States":
                                break;

                            case "State":
                                Console.WriteLine();
                                Console.WriteLine("State name = " + reader["Name"]);
                                Console.WriteLine("State capital = " + reader["Capital"]);
                                break;
                        }
                    }
                }
            }
        }

        // Display any validation errors.
        private static void ValidationCallback(object sender, ValidationEventArgs e)
        {
            Console.WriteLine(" *** Validation Error: {0}", e.Message);
        }
    }
}

