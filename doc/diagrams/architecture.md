@startuml

package "Core" {
    component [VersionSource]
    component [CurrentVersionDetecter]
    component [Downloader]   
    component [VersionSourceParser] 
    component [UpdaterManagementService] as Facade    
}

package "Updater" {
    component [UpdaterCore]
    component [FileCopyUpdater]
    component [MsiInstallerUpdater]
}

[UpdaterCore] --> [FileCopyUpdater]
[UpdaterCore] --> [MsiInstallerUpdater]

[Facade] ..> [UpdaterCore] : IPC

[Facade] --> [VersionSource]
[Facade] --> [Downloader]

[VersionSource] --> [VersionSourceParser]
[Facade] --> [CurrentVersionDetecter]

component [UI]
[UI] --> [Facade]














@enduml
