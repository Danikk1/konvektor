using System;
using System.IO;
using Newtonsoft.Json;
using System.Xml.Serialization;

class Program
{
    static void Main(string[] args)
    {
        TextEditor editor = new TextEditor();

        while (true)
        {
            Console.Clear();
            editor.DisplayText();

            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Открыть файл");
            Console.WriteLine("2. Сохранить файл");
            Console.WriteLine("3. Изменить текст");
            Console.WriteLine("4. Выйти");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Введите имя файла для открытия: ");
                    string fileName = Console.ReadLine();
                    editor.LoadFile(fileName);
                    break;

                case "2":
                    Console.Write("Введите имя файла для сохранения: ");
                    string saveFileName = Console.ReadLine();
                    editor.SaveFile(saveFileName);
                    break;

                case "3":
                    Console.Write("Введите новый текст: ");
                    string newText = Console.ReadLine();
                    editor.ChangeText(newText);
                    break;

                case "4":
                    Environment.Exit(0);
                    break;
            }
        }
    }
}

class TextEditor
{
    private string text;

    public void LoadFile(string fileName)
    {
        try
        {
            if (File.Exists(fileName))
            {
                string fileExtension = Path.GetExtension(fileName);

                switch (fileExtension)
                {
                    case ".txt":
                        text = File.ReadAllText(fileName);
                        break;

                    case ".json":
                        text = File.ReadAllText(fileName);
                        break;

                    case ".xml":
                        XmlSerializer serializer = new XmlSerializer(typeof(string));
                        using (StreamReader reader = new StreamReader(fileName))
                        {
                            text = (string)serializer.Deserialize(reader);
                        }
                        break;

                    default:
                        Console.WriteLine("Неподдерживаемый формат файла.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Файл не найден.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка при открытии файла: " + ex.Message);
        }
    }

    public void SaveFile(string fileName)
    {
        try
        {
            string fileExtension = Path.GetExtension(fileName);

            switch (fileExtension)
            {
                case ".txt":
                    File.WriteAllText(fileName, text);
                    Console.WriteLine("Файл сохранен.");
                    break;

                case ".json":
                    File.WriteAllText(fileName, JsonConvert.SerializeObject(text));
                    Console.WriteLine("Файл сохранен.");
                    break;

                case ".xml":
                    XmlSerializer serializer = new XmlSerializer(typeof(string));
                    using (StreamWriter writer = new StreamWriter(fileName))
                    {
                        serializer.Serialize(writer, text);
                    }
                    Console.WriteLine("Файл сохранен.");
                    break;

                default:
                    Console.WriteLine("Неподдерживаемый формат файла.");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка при сохранении файла: " + ex.Message);
        }
    }

    public void ChangeText(string newText)
    {
        text = newText;
    }
    public void DisplayText()
    {
        Console.WriteLine("Текстовый редактор:");
        Console.WriteLine(text);
    }
}