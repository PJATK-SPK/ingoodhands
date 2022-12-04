module "ingoodhands" {
  source = "../../modules/ingoodhands"

  google-project = var.google-project
  region           = var.region
}
