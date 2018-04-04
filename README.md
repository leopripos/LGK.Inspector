# LGK.Inspector
=====================================

This is simple inspector for Unity Game Engine for Svelto.ECS (https://github.com/sebas77/Svelto.ECS)

## Feature
* Svelto.ECS style (without MonoBehaviour)
* Using Reflection
* Support field
* Support property (setter/getter only)
* Support custom component drawer (like unity custom inspector)
* Support custom type drawer (like unity property drawer)
* Support array
* Support nested type (class and struct)
* \** Not Support List and Dictionary

\** need suggestion how to implement this :D

## Usage
```csharp
public interface IInspectorService
{
    void Register(int id, object[] components, byte modeMask = EInspectorMode.All);

    void Register(string group, int id, object[] components, byte modeMask = EInspectorMode.All);

    void Register(string group, IEntityInfo entityInfo);

    void Unregister(int id);

    void Unregister(string group, int id);
}

public static class InspectorUtility
{
    public static IEntityInfo ExtractEntityInfo(int id, object[] components, byte modeMask);
}

public static class EInspectorMode
{
    public const byte All = byte.MaxValue;
    public const byte Field = 1;
    public const byte Property = 1 << 1;
}
```

## Next Roadmap
* Support IList<>
* Support IDictionary<,>
* Make it more beautiful :)

## Examples
* Simple Example : https://github.com/NED-Studio/LGK.Inspector.Example

## Screenshot
<img src="https://raw.githubusercontent.com/NED-Studio/RepositoryResources/master/LGK.Inspector/Screenshot/Screenshot%20Example%201.PNG" height="500" alt="Screenshot 1"/><img src="https://raw.githubusercontent.com/NED-Studio/RepositoryResources/master/LGK.Inspector/Screenshot/Screenshot%20Example%202.PNG" height="500" alt="Screenshot 2"/>
