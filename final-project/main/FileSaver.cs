namespace main;

using System.IO;


public class FileSaver{

    string fileName;


    public FileSaver(string fileName){
        this.fileName = fileName;
        if (!File.Exists(fileName)){
            File.Create(this.fileName).Close();
        }
    }

    public void AppendLine(string line){
        File.AppendAllText(this.fileName, line + Environment.NewLine);
    }

    public void DeleteFile(){
        if(File.Exists(this.fileName)){
            File.Delete(this.fileName);
        }
    }

}