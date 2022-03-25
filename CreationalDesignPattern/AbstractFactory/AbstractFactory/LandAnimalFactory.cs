namespace AbstractFactory
{
    public class LandAnimalFactory : AnimalFactory
    {
        public override IAnimal GetAnimal(string animalType)
        {
            if (animalType == "Dog") return new Dog();
            else if (animalType == "Cat") return new Cat();
            else return new Lion();
        }
    }
  
}