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

abstract class VersionSource {

}

abstract class HttpVersionSource {
    url : string
}

abstract class FileSystemVersionSource {
    path : string
}

VersionSource <|-- HttpVersionSource
VersionSource <|-- FileSystemVersionSource

Version --> "1" VersionNumber
Version --> "1" VersionSource

@enduml