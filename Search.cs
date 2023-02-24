using System.ComponentModel.DataAnnotations;
public class Search{
    public string SearchString {get;set;}
    public int SearchID {get;set;}

    public Search(){
        SearchString = string.Empty;
    }

    public Search(string s){
        SearchString = s;
    }
}