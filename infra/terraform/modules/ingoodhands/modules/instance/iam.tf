resource "google_service_account" "service_account" {
  account_id   = format("indagoodhands-vm-sa-%s", var.environment)
  display_name = format("indagoodhands-vm-sa-%s", var.environment)
}

resource "google_artifact_registry_repository_iam_member" "repo_iam" {
  provider = google-beta
  project  = var.google-project

  location   = "europe"
  repository = "default-docker"
  role       = "roles/artifactregistry.reader"
  member     = format("serviceAccount:%s", google_service_account.service_account.email)
}
