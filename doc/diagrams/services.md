@startuml
!include diagrams/service_VersionSource.md


interface IUpdaterManagementService {
    Start()
    SearchVersion()   : UpdateVersionUserInteractionHandle      
}
class UpdaterManagementService {
    ExecuteUpdate(handle : UpdateVersionHandle) 
}
IUpdaterManagementService <|.. UpdaterManagementService


interface ICurrentVersionDeterminer {
    GetCurrentVersion() : VersionNumber
}



interface IVersionDownloader {

}
class HttpVersionDownloader {

}
class DoNothingVersionDownloader {
    
}
IVersionDownloader <|.. HttpVersionDownloader
IVersionDownloader <|.. DoNothingVersionDownloader




interface IUserInteraction {
    NewVersionAvailable(handle : IUpdateVersionHandle)
}
interface IUpdateVersionHandle {    
    HasNewVersion : bool
    NewVersion : Version    
    
    UpdateToVersion()
}
class UpdateVersionHandle {

}

IUpdateVersionHandle <|.. UpdateVersionHandle
UpdateVersionHandle <.. UpdaterManagementService : create
UpdateVersionHandle --> UpdaterManagementService
IUpdateVersionHandle <.. IUserInteraction : use

UpdaterManagementService --> "1..*" IVersionSource
UpdaterManagementService --> ICurrentVersionDeterminer
UpdaterManagementService ..> IVersionDownloader : use
UpdaterManagementService ..> IUserInteraction : call

interface IUpdatePrepartionStep {

}

@enduml