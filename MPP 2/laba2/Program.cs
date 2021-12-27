using System;
using System.IO;
using System.Threading;

namespace laba2
{
    public class MainClass
    {
		static TaskQueue queue;        

		public static void CreateDirs(string[] dirs, string oldPath, string newPath)
		{      
			for (int i = 0; i < dirs.Length; i++)
			{
				dirs[i] = dirs[i].Replace(oldPath, newPath);
				if (!Directory.Exists(dirs[i]))
				{
					Directory.CreateDirectory(dirs[i]);
				}
			}
		}

		public static string[] deleteDS_Store(string[] files)
		{
			int filesNum = files.Length;

			for (int i = 0; i < files.Length; i++)
			{
				if (Path.GetFileName(files[i]) == ".DS_Store")
				{
					filesNum--;
				}
			}

			string[] editedFiles = new string[filesNum];
			filesNum = 0;

			for (int i = 0; i < files.Length; i++)
            {
                if (Path.GetFileName(files[i]) != ".DS_Store")
                {
					editedFiles[filesNum++] = files[i];
                }
            }


			return editedFiles;
		}

		public static void copyFiles(string[] files, string oldPath, string newPath)
		{
			string destPath;
			for (int i = 0; i < files.Length; i++)
			{
				destPath = files[i].Replace(oldPath, newPath);
				if (!File.Exists(destPath))
				{
					queue.EnqueueTask(File.Copy, files[i], destPath);
				}
			}
		}

        public static void Main()
        {			
			string path1 = Console.ReadLine();
			string path2 = Console.ReadLine();

			string[] filesFromFirstDir  =  Directory.GetFiles(path1, "*", SearchOption.AllDirectories);
			string[] filesFromSecDir    =  Directory.GetFiles(path2, "*", SearchOption.AllDirectories);
			string[] dirsFromFirstDir   =  Directory.GetDirectories(path1, "*", SearchOption.AllDirectories);
			string[] dirsFromSecDir     =  Directory.GetDirectories(path2, "*", SearchOption.AllDirectories);

			filesFromSecDir = deleteDS_Store(filesFromSecDir);
			filesFromFirstDir = deleteDS_Store(filesFromFirstDir);

			CreateDirs(dirsFromSecDir, path2, path1);
            CreateDirs(dirsFromFirstDir, path1, path2);

			int filesCount = filesFromSecDir.Length + filesFromFirstDir.Length;

			queue = new TaskQueue(filesCount);

			copyFiles(filesFromSecDir, path2, path1);
			copyFiles(filesFromFirstDir, path1, path2);
            
			Thread.Sleep(100);
			Console.WriteLine("Files copied: {0}", filesCount);
        }
    }
}
