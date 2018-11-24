@startuml

package "Core" {
    component [VersionSource]
    component [CurrentVersionDetecter]
    component [Downloader]   
    component [VersionSourceParser] 
    component [Facade and Core] as Facade    
}

[Facade] --> [VersionSource]
[Facade] --> [Downloader]

[VersionSource] --> [VersionSourceParser]
[Facade] --> [CurrentVersionDetecter]

component [UI]
[UI] --> [Facade]














@enduml
