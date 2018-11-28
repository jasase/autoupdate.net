@startuml

class Version {
    ChangeLog : string
    mandatory : bool
}

class VersionNumber {
    Major : int
    Minor : int
    Revision : int
    Build : int
}

abstract class VersionDownloadSource {

}

abstract class HttpVersionDownloadSource {
    url : string
}

abstract class FileSystemVersionDownloadSource {
    path : string
}

VersionDownloadSource <|-- HttpVersionDownloadSource
VersionDownloadSource <|-- FileSystemVersionDownloadSource

Version --> "1" VersionNumber
Version --> "1" VersionDownloadSource

@enduml