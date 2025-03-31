public interface IContainer { 
    double CargoWeight { get; }
    double Height { get; }
    double Depth { get; }
    double Width { get; }
    double MaxPayload { get; }
    double TareWeight { get; }
    string SerialNumber { get; }

    void Load(double weight);
    void Unload();
}