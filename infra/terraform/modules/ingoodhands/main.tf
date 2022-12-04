module "basics" {
  source = "./modules/basics"

  google-project = var.google-project
  region           = var.region
}
