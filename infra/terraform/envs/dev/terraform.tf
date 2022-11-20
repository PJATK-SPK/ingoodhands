terraform {
  required_version = "~>1.0.9"

  backend "gcs" {
    bucket = "indagoodhandsdev-terraform"
    prefix = "state"
  }

  required_providers {
    google = {
      source  = "hashicorp/google"
      version = "~>4.30"
    }
    google-beta = {
      source  = "hashicorp/google-beta"
      version = "~>4.30"
    }
    googleworkspace = {
      version = "~>0.6"
    }
  }
}

provider "google" {
  project = var.google-project
  region  = var.region
  zone    = var.zone
}

provider "google-beta" {
  project = var.google-project
  region  = var.region
  zone    = var.zone
}
