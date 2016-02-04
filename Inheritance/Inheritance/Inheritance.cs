// Written by Joe Zachary for CS 3500, January 2015
// Revised by Joe Zachary, February 3, 2016
using System;

namespace Inherit
{
    /// <summary>
    /// Demonstrates aspects of inheritance and interfaces
    /// </summary>
    public class Tester
    {
        /// <summary>
        /// Demonstrates aspects of inheritance and interfaces
        /// </summary>
        public static void Main()
        {
            // Dog stored in Dog variable
            Dog dog = new Dog("Spot", "mutt");
            Console.WriteLine("Dog stored as Dog says " + dog.Speak() + " and shouts " + dog.Shout());
            Console.ReadLine();

            // Dog stored in Animal variable
            Animal animal1 = new Dog("Spot", "mutt");
            Console.WriteLine("Dog stored as Animal says " + animal1.Speak() + " and shouts " + animal1.Shout());
            Console.ReadLine();

            // Spider stored in Spider variable
            Spider spider = new Spider("Charlotte", false);
            Console.WriteLine("Spider stored as Spider says " + spider.Speak() + " and shouts " + spider.Shout());
            Console.ReadLine();

            // Spider stored in Animal variable
            Animal animal2 = new Spider("Charlotte", false);
            Console.WriteLine("Spider stored as Animal says " + animal2.Speak() + " and shouts "  + animal2.Shout());
            Console.ReadLine();

            // Phone stored as Phone
            Phone phone = new Phone();
            Console.WriteLine("Phone stored as Phone says " + phone.Speak());
            Console.ReadLine();

            // All of the above, stored in an array of Speakers
            Console.WriteLine("All speak simultaneously, in the same order, after being stored as Speakers:");
            Console.WriteLine(SpeakSimultaneously(new Speaker[] { dog, animal1, spider, animal2, phone }));
            Console.ReadLine();
        }

        /// <summary>
        /// Appends the result of all the speakers speaking and returns the resulting string.
        /// </summary>
        public static String SpeakSimultaneously(Speaker[] speakers)
        {
            String result = "";
            foreach (Speaker s in speakers)
            {
                result += s.Speak() + " ";
            }
            return result;
        }

    }

    /// <summary>
    /// An interface that requires a Speak() method.
    /// </summary>
    public interface Speaker
    {
        /// <summary>
        /// Returns what the Speaker has to say
        /// </summary>
        String Speak();
    }


    /// <summary>
    /// An Animal object represents various aspects of a real
    /// animal.  This is an abstract class, so an Animal
    /// cannot be directly constructed.  Instead, you must
    /// instantiate one of its derived classes.
    /// </summary>
    public abstract class Animal : Speaker
    {
        /// <summary>
        /// Create an Animal with the specified name.
        /// </summary>
        public Animal(String n)
        {
            Name = n;
        }

        /// <summary>
        /// Obtain the name of the Animal.
        /// </summary>
        public String Name { get; private set; }

        /// <summary>
        /// All Animals say something.  This is abstract, which
        /// will force derived classes to override.
        /// </summary>
        /// <returns></returns>
        public abstract String Speak();

        /// <summary>
        /// An upper-case version of what the animal Speaks.
        /// Note that it is not virtual, which means that it
        /// cannot be overriden.
        /// </summary>
        /// <returns></returns>
        public String Shout()
        {
            return Speak().ToUpper();
        }

        /// <summary>
        /// The number of legs that an animal has.
        /// It is virtual, so it can be overridden.
        /// </summary>
        public virtual int LegCount
        {
            get { return 4; }
        }

        /// <summary>
        /// When n <= 0, throws an ArgumentException.  Otherwise,
        /// returns a string composed of n copies of what this animal
        /// says when it speaks, with the copies separated by white
        /// space.  It is virtual, so it can be overridden.  
        /// </summary>
        public virtual String SpeakRepeatedly(int n)
        {
            if (n <= 0)
            {
                throw new ArgumentException();
            }
            String result = Speak();
            while (n > 0)
            {
                result += " " + Speak();
                n--;
            }
            return result;
        }
    }


    /// <summary>
    /// A Dog is a kind of Animal
    /// </summary>
    public class Dog : Animal
    {
        /// <summary>
        /// A Dog has a name and a breed
        /// </summary>
        public Dog(String n, String b)
            : base(n)
        {
            Breed = b;
        }

        /// <summary>
        /// Obtain the breed of the Dog
        /// </summary>
        public String Breed { get; private set; }

        /// <summary>
        /// What a dog says.
        /// </summary>
        /// <returns></returns>
        public override String Speak()
        {
            return "woof";
        }

        /// <summary>
        /// Behaves like the overridden method, but returns the
        /// empty string when n <= 0
        /// </summary>
        public override string SpeakRepeatedly(int n)
        {
            if (n <= 0)
            {
                return "";
            }
            else
            {
                return base.SpeakRepeatedly(n);
            }
        }
    }


    /// <summary>
    /// A Spider is a kind of Animal
    /// </summary>
    public class Spider : Animal
    {
        /// <summary>
        /// A Spider has a name and may be poisonous
        /// </summary>
        public Spider(String n, bool p)
            : base(n)
        {
            IsPoisonous = p;
        }

        /// <summary>
        /// Find out whether the spider is poisonous
        /// </summary>
        public bool IsPoisonous { get; private set; }


        /// <summary>
        /// This property is overridden and sealed.  It cannot
        /// be overrriden by derived classes.
        /// </summary>
        public override sealed int LegCount
        {
            get
            {
                return 8;
            }
        }

        /// <summary>
        /// What a spider says.
        /// </summary>
        /// <returns></returns>
        public override String Speak()
        {
            return "ick";
        }

        /// <summary>
        /// Attempt to override the non-virtual Shout.
        /// </summary>
        /// <returns></returns>
        public new string Shout()
        {
            return base.Shout() + "!!!!!!";
        }

        /// <summary>
        /// Behaves like the overridden method, but puts two spaces
        /// between each copy of Speak()
        /// </summary>
        public override string SpeakRepeatedly(int n)
        {
            if (n <= 0)
            {
                throw new ArgumentException();
            }
            String result = Speak();
            while (n > 0)
            {
                result += "  " + Speak();
                n--;
            }
            return result;
        }
    }

    /// <summary>
    /// A Phone isn't an animal, but it is a speaker.
    /// </summary>
    public class Phone : Speaker
    {
        public String Speak()
        {
            return "Siri";
        }
    }
}
