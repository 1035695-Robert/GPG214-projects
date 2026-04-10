using UnityEditor.Experimental.GraphView;
using UnityEngine;
namespace SimpleFactory
{
    public abstract class Item
    {
        public abstract void Generate();
    }
    public class Red : Item
    {
        public override void Generate()
        {

        }
    }
    public class Blue : Item
    {
        public override void Generate()
        {

        }
    }
    public class Green : Item
    {
        public override void Generate()
        {

        }
    }
    public class Black : Item
    {
        public override void Generate()
        {

        }
    }
    public class ItemFactory
    {
        public Item GetItem(string hexColor)
        {
            switch (hexColor)
            {
                case "00FF00FF": // green = BoxLoaders
                    Debug.Log("GREEN");
                    return new Green();

                case "0000FFFF": //Blue = Belt
                    Debug.Log("BLUE ");
                    return new Blue();

                case "FF0000FF": //Red = Goals
                                 //Debug.Log("RED");
                    return new Red();

                case "00000000": //black = Obstacles
                    return new Black();
                
                default:
                    return null;
            }
        }
    }
}