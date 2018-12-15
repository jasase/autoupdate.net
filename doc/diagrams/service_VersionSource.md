@startuml

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



@enduml