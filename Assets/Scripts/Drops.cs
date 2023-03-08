using System.Collections.Generic;
public class Weapon {
    public string name;
    public string type;
    public int damage;
    public string abilityName;
}

public class Armor {
    public string name;
    public string type;
    public int defense;
    public string passiveName;
}

public class WeaponList {
    public List<Weapon> Weapon;
}

public class ArmorList {
    public List<Armor> Armor;
}