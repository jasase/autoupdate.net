@startuml

package "Core" {
    component [VersionSource]
    component [CurrentVersionDetecter]
    component [Downloader]   
    component [VersionSourceParser] 
    component [UpdaterManagementService] as Facade    
    component [UpdatePreparationSteps] 
}

package "UpdateExecuter" {
    component [Executer]
    component [ExecuterSteps]    
}

[Executer] --> [ExecuterSteps]

[Facade] ..> [Executer] : IPC

[Facade] --> [VersionSource]
[Facade] --> [Downloader]
[Facade] --> [UpdatePreparationSteps]

[VersionSource] --> [VersionSourceParser]
[Facade] --> [CurrentVersionDetecter]

component [UI]
[UI] --> [Facade]














@enduml
