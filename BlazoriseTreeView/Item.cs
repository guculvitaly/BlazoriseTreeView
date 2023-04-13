using System.Text.Json;

namespace BlazoriseTreeView
{
    public class Item
    {
        public Item()
        {
           
        }
        public Item(string checkedNodes)
        {
            CheckedNodes = new List<string>() { checkedNodes };
        }
        public int? ParentId { get; set; }
        public string NodeId { get; set; }
        public string Text { get; set; }
        /// <summary>
        /// Caret up or down
        /// </summary>
        public bool ShowItem { get; set; }
        public bool IsChecked { get; set; }
        public string CaretClass => AddOrRemoveCaretClass();
        public string ShowOrHideUl => !ShowItem ? "hideChild" : string.Empty;

        public List<Item> Children { get; set; }
        public  List<string> CheckedNodes = new List<string>();

        private string AddOrRemoveCaretClass()
        {
            if (ShowItem)
            {
                return "fas fa-angle-down";
            }if  (Children == null || !Children.Any())
            {
                return string.Empty;
            }
            else
            {
                return "fas fa-angle-right";
            }
            
        }




        //private void AddNode(List<Item> data, int parentId)
        //{
        //    var parent = data.FirstOrDefault(x => x.ParentId == parentId);
        //    var dataItems = data.Where(x => x.Parent == parent);
        //    foreach (var dataItem in dataItems)
        //    {
        //        AddNode(data, dataItem.ID);
        //    }
        //    Tree.Nodes.Add(parent.Name);
        //}
    }


    
}
