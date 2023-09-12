
using CommandLine;

class Program
{
    static void Main(String[] args)
    {
        //Checks if the comand has three arguments.
        if (args.Length == 3)
        {
            Logger logger = new();
            string sourcePath = args[0];
            int synchInterval = int.Parse(args[1]);
            string replicaPath = args[2];
            bool directoryExists = Directory.Exists(replicaPath);
            bool sourcePathExists = Directory.Exists(sourcePath);

            //checks if the source path exists, otherwise a message is sent to the terminal.
            if (!sourcePathExists)
                Console.WriteLine($"The Path '{replicaPath}' is not correct.");
            else
            {
                if (!directoryExists) //checks if the directory exists, otherwise one is created with the chosen name.
                {
                    Console.WriteLine($"{replicaPath} Created.");
                    logger.WriteLog($"{replicaPath} Created.");
                    Directory.CreateDirectory(replicaPath);
                }
                while (true) //an infinite loop is created to run the synchronization method and then wait for the pre-established interval
                {
                    Synchronizer(sourcePath, replicaPath);
                    Thread.Sleep(synchInterval);
                }
            }
        }
        else //if an argument is missing a message is sent to the terminal
        { 
            Console.WriteLine("Please insert: Source Path, Synchronization Interval,  Replica path.");
        }
    }

    static void Synchronizer(string sourcePath, string replicaPath)
    {
        Logger logger = new();
        String[] sourceFiles = Directory.GetFiles(sourcePath);
        String[] replicaFiles = Directory.GetFiles(replicaPath);


        foreach (string file in sourceFiles)
        {
            //Each file is copied to the replica path and if overwritten if has the same name.
            string fileName = Path.GetFileName(file);
            File.Copy(file, $"{replicaPath}/{Path.GetFileName(file)}", true);
            //All the action is written in the terminal and the log file.
            Console.WriteLine($"{ fileName } copied.");
            logger.WriteLog($"{ fileName } copied.");
        }

        foreach (string replicaFile in replicaFiles)
        {
            //checks if there are files deleted in the source path and deleted if is true.
            string fileName = Path.GetFileName(replicaFile);
            string fileSourcePath = Path.Combine(sourcePath, fileName);
            if (!File.Exists(fileSourcePath))
            {
                File.Delete(replicaFile);
                Console.WriteLine($"{ fileName } deleted.");
                logger.WriteLog($"{fileName} deleted.");
            }
        }
        Console.WriteLine("Synchronization complete!");
    }
}

