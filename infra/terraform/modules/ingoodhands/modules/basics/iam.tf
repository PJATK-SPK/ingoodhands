resource "google_service_account" "cicd" {
  account_id   = "gha-cicd"
  display_name = "CICD Service Account"
}
#Artifacts Registry
resource "google_project_iam_member" "cicd-ar" {
  project = var.google-project
  member  = format("serviceAccount:%s", google_service_account.cicd.email)
  role    = "roles/artifactregistry.writer"
}
#Firebase
resource "google_project_iam_member" "cicd-firebase" {
  project = var.google-project
  member  = format("serviceAccount:%s", google_service_account.cicd.email)
  role    = "roles/firebase.admin"
}
#Cloud Run
resource "google_service_account" "cloud-run" {
  account_id   = "cloud-run"
  display_name = "Cloud Run Service Account"
}

resource "google_project_iam_member" "cloud-run-sql" {
  project = var.google-project
  member  = format("serviceAccount:%s", google_service_account.cicd.email)
  role    = "roles/cloudsql.client"
}

resource "google_project_iam_member" "cicd-cloud-run" {
  project = var.google-project
  member  = format("serviceAccount:%s", google_service_account.cicd.email)
  role    = "roles/run.admin"
}

resource "google_service_account_iam_member" "cicd-cloud-run-act-as" {
  service_account_id = google_service_account.cloud-run.name
  role               = "roles/iam.serviceAccountUser"
  member             = format("serviceAccount:%s", google_service_account.cicd.email)
}

resource "google_service_account" "worker" {
  account_id   = "worker"
  display_name = "Worker Cloud Run Service Account"
}

resource "google_project_iam_member" "worker" {
  project = var.google-project
  member  = format("serviceAccount:%s", google_service_account.worker.email)
  role    = "roles/run.invoker"
}
