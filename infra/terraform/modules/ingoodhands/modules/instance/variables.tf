variable "environment" {
  type = string
}

variable "machine-type" {
  type = string
  default = "e2-micro"
}

variable "zone" {
  type = string
  default = "europe-west3-b"
}

variable "google-project" {
  type = string
}
