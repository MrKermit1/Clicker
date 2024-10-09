using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Clicker.Models
{
    public class ClickerModel : INotifyPropertyChanged
    {


        private string name = string.Empty;
        private int value;
        private int initValue;
        private string color = string.Empty;

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;


        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                }
            }
        }

        public int Value
        {
            get => value;
            set
            {
                if (this.value != value)
                {
                    this.value = value ;
                    OnPropertyChanged();
                }
            }
        }

        public int InitValue
        {
            get => initValue;
            set
            {
                if (this.initValue != value)
                {
                    this.initValue = value;
                }
            }
        }

        public string Color
        {
            get => color;
            set
            {
                if (color != value)
                {
                    color = value;
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Increment()
        {
            Value++;
        }

        public void Decrement() 
        {
            Value--;
        }

        public void Reset() => Value = InitValue;

        public bool isNameEmpty()
        {
            if (Name == "" || Name == String.Empty || string.IsNullOrWhiteSpace(Name))
            {
                return true;
            }

            return false;
        }

    }
}
