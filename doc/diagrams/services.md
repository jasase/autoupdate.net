@startuml

interface IUpdaterManagementService {
    Start()
    SearchVersion()   : UpdateVersionUserInteractionHandle  
    ~ UpdateToVersion(handle : UpdateVersionUserInteractionHandle)   
}
class UpdaterManagementService {
    
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


interface IVersionSource {
    LoadAvailableVersion() : Version[]
}
class HttpVersionSource { 
}
class FileVersionSource { 
}
class SpecialVersionSource { 
}
IVersionSource <|.. HttpVersionSource
IVersionSource <|.. FileVersionSource
IVersionSource <|.. SpecialVersionSource


interface IVersionParser {
    ParseVersions(content : string) : Version[]
}
class XmlVersionParser { 
}
class JsonVersionParser { 
}
IVersionParser <|.. XmlVersionParser
IVersionParser <|.. JsonVersionParser


HttpVersionSource --> IVersionParser
FileVersionSource --> IVersionParser


interface IUserInteraction {
    NewVersionAvailable(handle : UpdateVersionUserInteractionHandle)
}
class UpdateVersionUserInteractionHandle {    
    HasNewVersion : bool
    NewVersion : Version    
    
    UpdateToVersion()
}

UpdateVersionUserInteractionHandle ..> IUpdaterManagementService : use

UpdaterManagementService --> "1..*" IVersionSource
UpdaterManagementService --> ICurrentVersionDeterminer
UpdaterManagementService ..> IVersionDownloader : use
UpdaterManagementService ..> IUserInteraction : call



@enduml