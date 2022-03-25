namespace AbstractFactory
{
    internal class SeaAnimalFactory : AnimalFactory
    {
        public override IAnimal GetAnimal(string animalType)
        {
            IAnimal animal = null;
            if (animalType == "Octopus")
                animal = new Octopus();
            else if (animalType == "Shark")
                animal = new Shark();
            return animal;
        }
    }
}