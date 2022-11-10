resource "google_project_service" "artifact_registry_api" {
  project = var.google-project
  service = "artifactregistry.googleapis.com"
  disable_on_destroy = false
}

resource "google_artifact_registry_repository" "artifact_registry_docker_repo" {
  provider = google-beta
  project  = var.google-project
  location = "europe"

  format        = "DOCKER"
  repository_id = "default-docker"
}

resource "google_service_account" "cicd_service_account" {
  account_id   = "cicd-service-account"
  display_name = "CICD Service Account"
}

resource "google_project_iam_member" "cicd_service_account_iam" {
  project = var.google-project
  member  = format("serviceAccount:%s", google_service_account.cicd_service_account.email)
  role    = "roles/artifactregistry.writer"
}