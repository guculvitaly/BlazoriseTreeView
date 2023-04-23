namespace BlazoriseTreeView
{
    public class CheckBoxListItem<T>
    {
        public bool Selected { get; set; }
        public string Name { get; set; }
        public List<CheckBoxListItem<T>> Children { get; set; } = new List<CheckBoxListItem<T>>();
        public T Data { get; set; }
    }
}
