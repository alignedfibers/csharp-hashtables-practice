using System;
using System.Text;
using System.Collections;


namespace chap9.zookeeper{

class zooAnimalID{
	private readonly char prefix;
	private readonly int num;
	
	public zooAnimalID(string id){
			
		prefix = (id.ToUpper())[0];
		num = int.Parse(id.Substring(1,3));
		
	}
	
	public override string ToString()
	{
		return prefix.ToString() + string.Format("{0,3:000}", num);
	}
	
	public override int GetHashCode()
	{
		string str = this.ToString();
		return str.GetHashCode();		
	}
	
	public override bool Equals(object obj){
			/**************************************
			Recieves any object then casts it to 
			a zooAnimalID object. If the prefix is
			and the numis the same as this 
			zooAnimalID instance it will return true, 
			else false
			***************************************/
			
			zooAnimalID aAnimal = obj as zooAnimalID;
			
			if(aAnimal == null)
				return false;
			if(prefix == aAnimal.prefix && num == aAnimal.num)
				return true;
			return false;
	}
}

class zooAnimalData{
	private string name;
	private string species;
	private decimal budget;
	private zooAnimalID id;

	public zooAnimalData(zooAnimalID id, string name, decimal budget, string species ){
	this.name = name;
	this.budget = budget;
	this.species = species;
	this.id = id;
	}
	
	public override string ToString(){
			StringBuilder sb = new StringBuilder("ID: "+id.ToString(), 100);
			sb.Append(": Name: ");
			sb.Append(string.Format("{0, -8}", name));
			sb.Append(" Species: ");
			sb.Append(string.Format("{0, -8}", species));
			sb.Append(" Feed Cost: ");
			sb.Append(string.Format("{0:C}", budget));
			return sb.ToString();
	}
	

}

class zooAnimalTable{

			Hashtable animals = new Hashtable(31);
			
			public void Run(){
				zooAnimalID idCow = new zooAnimalID("B001");
				zooAnimalData cow = new zooAnimalData(idCow, "Mr Cow", 100000.00M, "Farm");
				zooAnimalID idDolphin = new zooAnimalID("W234");
				zooAnimalData dolphin = new zooAnimalData(idDolphin, "flipper", 10000.00M, "Sea Creature");

				animals.Add(idCow, cow);
				animals.Add(idDolphin, dolphin);
				
				while(true){
					try
					{
						Console.WriteLine("Enter zoo animal ID (format:A999, X to exit, DISPLAY ALL to display all, or ADD to add another animal)>");
						string userInput = Console.ReadLine();
						userInput = userInput.ToUpper();
						
						if(userInput == "X"){return;}
						
						else if(userInput == "ADD"){
							bool lc = true;
							while(lc){
							lc = addAnimal();
							}
						}
						else if(userInput == "DISPLAY ALL"){
							dispAll();
						}else{
						
						zooAnimalID id = new zooAnimalID(userInput);
						
						DisplayData(id);
						}
					}
					catch(Exception e){
						Console.WriteLine("Exception occured did you use the right format for zoo animal ID?");
						Console.WriteLine(e.ToString());
					}
				}
			}
					
			private bool addAnimal(){
						bool lc = true;
						try{
						Console.WriteLine("Enter an animal name");
						string nam = (string)Console.ReadLine();
						Console.WriteLine("Enter a species");
						string spec = (string)Console.ReadLine();
						Console.WriteLine("Enter an id 1 char + 3 digit int");
						string identity = (string)Console.ReadLine();
						Console.WriteLine("Enter the budget for this animal as decimal");
						decimal budg =decimal.Parse(Console.ReadLine());
						Console.WriteLine("Processing animal");
						zooAnimalID idHld = new zooAnimalID(identity);
						zooAnimalData hld = new zooAnimalData(idHld, nam, budg, spec);
						animals.Add(idHld, hld);
						Console.WriteLine("Your animal has been added, To add another animal enter y or n");
						string enter = Console.ReadLine();
						
						if(enter == "y"){
							lc = true;
						}
						else if(enter == "n"){
							lc = false;
						}
						else{
						Console.WriteLine("You most likely mistakingly entered an incorrect value, and will by default have to enter another animal");
						}
						
						
						}catch(Exception e){
							Console.WriteLine(e.ToString());
						}
						
						return lc;
					
			}
			
			public void dispAll(){
			
			foreach(DictionaryEntry d in animals)
			
				if(d.Value != null){
				
				zooAnimalData zooAnm = (zooAnimalData)d.Value;
				
				Console.WriteLine("Zoo Animal: "+zooAnm.ToString());
				}
			}
				
			
			
			private void DisplayData(zooAnimalID id){
				object anmObj = animals[id];
				
				if(anmObj != null){
					zooAnimalData anAnimal = (zooAnimalData)anmObj;
					
					Console.WriteLine("Zoo Animal: "+anAnimal.ToString());
				}
				else{
					Console.WriteLine("Employee not found: ID = "+id);
				}
			}
}

class zooAnimalMain{
		static void Main(){
		
	
			zooAnimalTable anmTbl = new zooAnimalTable();
			
			anmTbl.Run();
		}

}

}