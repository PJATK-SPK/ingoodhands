resource "google_service_account" "cicd_service_account" {
  account_id   = "cicd-service-account"
  display_name = "CICD Service Account"
}

resource "google_project_iam_member" "cicd_service_account_ar_iam" {
  project = var.google-project
  member  = format("serviceAccount:%s", google_service_account.cicd_service_account.email)
  role    = "roles/artifactregistry.writer"
}

resource "google_project_iam_member" "cicd_service_account_firebase_iam" {
  project = var.google-project
  member  = format("serviceAccount:%s", google_service_account.cicd_service_account.email)
  role    = "roles/firebase.admin"
}