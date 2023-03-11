using System.Collections.Generic;
public class Weapon {
    public string name;
    public string type;
    public int damage;
    public float critChance;
    public float critDmg;
    public int[] ability;
}

public class Armor {
    public string name;
    public string type;
    public int defense;
    public float evasion;
    public int[] passive;
}

public class WeaponList {
    public List<Weapon> Weapon;
}

public class ArmorList {
    public List<Armor> Armor;
}