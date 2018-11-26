@startuml

interface IUpdaterManagementService {
    
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

UpdateService --> "1..*" IVersionSource
UpdateService --> ICurrentVersionDeterminer
UpdateService ..> IVersionDownloader : use

interface IUserInteraction {
    NewVersionAvailable(handle : UpdateVersionUserInteractionHandle)
}
class UpdateVersionUserInteractionHandle {

}
UpdaterManagementService ..> IUserInteraction : call


@enduml