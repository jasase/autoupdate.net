@startuml

package "Core" {
    interface "IUpdateService" as US    
    interface "IVersionSource" as VS 
    interface "IVersionDownloader" as VD

    component [VersionSource]
    component [CurrentVersionDetecter]
    component [Downloader]   
    component [VersionSourceParser] 
    component [Facade and Core] as Facade    
}

US - [Facade]
VS - [VersionSource]
VD - [Downloader]

[Facade] ..> VS
[Facade] ..> VD


component [UI]
[UI] ..> US














@enduml
